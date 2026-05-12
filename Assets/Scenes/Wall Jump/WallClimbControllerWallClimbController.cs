using UnityEngine;

public class NinJumpController : MonoBehaviour
{
    [Header("Physics Settings")]
    public float jumpForce = 12f;      // قوة القفز للأعلى
    public float pushForce = 10f;      // قوة الدفع للجدار المقابل
    public float slideSpeed = 2f;      // سرعة الانزلاق للأسفل

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isTouchingWall = false;
    private bool isOnLeftWall = true;

    // عداد القفزات لضمان عدم تجاوز قفزتين
    private int jumpCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. تغيير زر القفز للمسافة (Space) مع شرط القفزتين
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            Jump();
        }

        // 2. تفعيل الانزلاق عند ملامسة الجدار
        if (isTouchingWall)
        {
            // نحدد سرعة الهبوط بـ slideSpeed لضمان الانزلاق التلقائي
            rb.linearVelocity = new Vector2(0, -slideSpeed);
            rb.gravityScale = 0; // تعطيل الجاذبية ليكون الانزلاق ثابتاً ومحكوماً بـ slideSpeed

            // تصغير عداد القفزات عند ملامسة الجدار (ليتمكن من القفز مجدداً)
            jumpCount = 0;
        }
        else
        {
            rb.gravityScale = 3; // الجاذبية تعمل فقط أثناء الطيران
        }
    }

    void Jump()
    {
        isTouchingWall = false;
        jumpCount++; // زيادة عداد القفزات عند كل ضغطة

        float direction = isOnLeftWall ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * pushForce, jumpForce);

        isOnLeftWall = !isOnLeftWall;
        spriteRenderer.flipX = !isOnLeftWall;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            jumpCount = 0; // إعادة ضبط القفزات بمجرد لمس الجدار
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