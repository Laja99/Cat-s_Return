using UnityEngine;

public class simple_move : MonoBehaviour
{
    public float speed = 5f;          
    public float jumpForce = 7f;      
    public Transform groundCheck;     
    public float groundRadius = 0.2f; 
    public LayerMask groundLayer; // تأكد من اختيار "Ground" من المفتش (Inspector)

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount = 0; // عداد القفزات

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // الحركة الأفقية (A, D أو الأسهم)
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // التحقق إذا كان اللاعب يلمس لاير الـ Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // تصفير العداد بمجرد ملامسة الأرض أو الجدار (طالما أنه في لاير Ground)
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // القفز باستخدام زر المسافة (Space) بشرط عدم تجاوز قفزتين
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // تصفير السرعة العمودية قبل القفزة الثانية لضمان قوة قفز ثابتة
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++; 
        }
    }
}