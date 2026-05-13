using UnityEngine;

public class ProCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;        // كائن اللاعب

    [Header("Follow Speeds")]
    [Range(0.01f, 1.0f)]
    public float smoothTime = 0.15f;    // سرعة الكاميرا العادية (أثناء الصعود)
    [Range(0.01f, 1.0f)]
    public float fallSmoothTime = 0.4f; // سرعة الكاميرا عند السقوط (أعلى = تأخير أكثر)

    [Header("Offset & Look Ahead")]
    public Vector3 offset = new Vector3(0, 2, -10); // Y تزيد من سبق الكاميرا للاعب

    [Header("Movement Axes")]
    public bool followHorizontal = true; // هل تتبع اللاعب يميناً ويساراً؟
    public bool followVertical = true;   // هل تتبع اللاعب صعوداً ونزولاً؟

    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;

    void Start()
    {
        // الحصول على Rigidbody الخاص باللاعب لاكتشاف السقوط
        if (target != null)
            rb = target.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. تحديد أي "سرعة تنعيم" نستخدم بناءً على حركة اللاعب
        float currentSmoothTime = smoothTime;
        
        if (rb != null && rb.linearVelocity.y < -0.5f) 
        {
            // إذا كان اللاعب يسقط، نستخدم التوقيت الأبطأ لتوضيح السقوط
            currentSmoothTime = fallSmoothTime;
        }

        // 2. تحديد الموقع المستهدف بناءً على الخيارات
        float targetX = followHorizontal ? target.position.x + offset.x : transform.position.x;
        float targetY = followVertical ? target.position.y + offset.y : transform.position.y;
        
        Vector3 targetPosition = new Vector3(targetX, targetY, offset.z);

        // 3. تطبيق التنعيم باستخدام التوقيت المختار
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, currentSmoothTime);
    }
}