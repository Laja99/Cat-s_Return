using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [Header("Wall Mechanics")]
    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [Header("Double Jump")]
    private int jumpCount = 0; // عداد القفزات لحساب القفزة المزدوجة

    [Header("Physics Checks")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private void Update()
    {
        // 1. الحركة الأفقية بالأسهم فقط
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1f;
        }
        else
        {
            horizontal = 0f;
        }

        // تحديث الفحوصات الفيزيائية للأرض والجدران
        if (IsGrounded() || IsWalled())
        {
            jumpCount = 0; // تصفير عداد القفز المزدوج عند ملامسة الأرض أو الجدار
        }

        // 2. نظام القفز (الأساسي والمزدوج وعلى الجدران) باستخدام زر المسافة
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // القفز من على الجدار (له الأولوية إذا كان اللاعب يتزحلق)
            if (wallJumpingCounter > 0f)
            {
                isWallJumping = true;
                rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;
                jumpCount = 1; // نعتبرها القفزة الأولى حتى يتمكن من القفز المزدوج بعدها إن أراد

                if (transform.localScale.x != wallJumpingDirection)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
            // القفز العادي من الأرض أو القفز المزدوج في الهواء
            else if (IsGrounded() || jumpCount < 2)
            {
                // تصفير السرعة العمودية قبل القفزة الثانية لضمان قوة ثابتة
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                jumpCount++;
            }
        }

        // تقصير القفزة عند ترك زر المسافة مبكراً ليعطي سلاسة (Game Feel)
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        WallSlide();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // تحريك اللاعب أفقياً طالما أنه لا يقفز حالياً من على جدار
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
            
            // تجهيز بيانات القفز من الجدار أثناء التزحلق
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            isWallSliding = false;
            if (!isWallSliding)
            {
                wallJumpingCounter -= Time.deltaTime;
            }
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}