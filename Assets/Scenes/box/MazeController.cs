using UnityEngine;

public class MazeRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // سرعة دوران المتاهة

    void Update()
    {
        // استخدام الأسهم أو AD لتدوير المتاهة حول محور Z
        float rotationInput = -Input.GetAxis("Horizontal"); 

        // تدوير المتاهة 360 درجة بناءً على المدخلات
        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime);
        
        // نصيحة: اترك الجاذبية ثابتة في إعدادات المشروع (0, -9.8)
        // Physics2D.gravity = new Vector2(0, -9.8f);
    }
}