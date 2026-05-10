using UnityEngine;

public class simple_move : MonoBehaviour
{
    public float speed = 5f;          // ���� �����
    public float jumpForce = 7f;      // ��� �����
    public Transform groundCheck;     // ���� ��� �����
    public float groundRadius = 0.2f; // ��� ����� �����
    public LayerMask groundLayer;     // ���� �����

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���� ���� �����
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // ��� ��� ������ ��� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // �����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}