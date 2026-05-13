using UnityEngine;

public class NinJumpController : MonoBehaviour
{
    [Header("Physics Settings")]
    public float jumpForce = 12f;      
    public float pushForce = 10f;      // القوة الجانبية عند القفز من الجدار
    public float airSideSpeed = 8f;    // القوة الجانبية الإضافية في الهواء (القفزة الثانية)
    public float slideSpeed = 2f;      

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isTouchingWall = false;
    private bool isOnLeftWall = true;
    private int jumpCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            Jump();
        }

        if (isTouchingWall)
        {
            rb.linearVelocity = new Vector2(0, -slideSpeed);
            rb.gravityScale = 0; 
            jumpCount = 0;
        }
        else
        {
            rb.gravityScale = 3; 
        }
    }

    void Jump()
    {
        isTouchingWall = false;
        jumpCount++; 

        // التعديل هنا:
        // إذا كانت هذه القفزة الثانية (في الهواء)، نستخدم سرعة جانبية مختلفة
        float currentPush = (jumpCount > 1) ? airSideSpeed : pushForce;

        float direction = isOnLeftWall ? 1 : -1;
        
        // تطبيق السرعة الجانبية (currentPush) والعمودية (jumpForce)
        rb.linearVelocity = new Vector2(direction * currentPush, jumpForce);

        isOnLeftWall = !isOnLeftWall;
        spriteRenderer.flipX = !isOnLeftWall;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            jumpCount = 0; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}