using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollection : MonoBehaviour // يفضل دائماً جعل أول حرف كابيتال
{
    void OnTriggerEnter2D(Collider2D col)
    {
        // هنا قمنا بتغيير التاج ليفحص إذا كان العنصر الذي دخل البوابة هو سمكة
        if (col.CompareTag("Fish"))
        {
            // زيادة عداد الأسماك المخرجة
            FishManager.instance.AddFish();
            
            // تدمير السمكة التي خرجت من المتاهة
            Destroy(col.gameObject); 
        }
    }
}