using UnityEngine;
using TMPro;
// خطوة مهمة: يجب استدعاء هذه المكتبة لتتمكن من تغيير السين
using UnityEngine.SceneManagement; 

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    public int fishCount = 0;
    public TextMeshProUGUI fishText;

    [Header("Win System")]
    public int totalFishInLevel = 3; // اكتب هنا عدد الأسماك الكلي الموجود في المرحلة
    public string winSceneName = "WinScene"; // اكتب هنا اسم سين شاشة الفوز بدقة

    void Awake()
    {
        instance = this;
    }

    public void AddFish()
    {
        fishCount++;
        fishText.text = fishCount.ToString();

        // الفحص: إذا وصل عدد الأسماك المخرجة إلى العدد الكلي للمرحلة
        if (fishCount >= totalFishInLevel)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("تم إخراج جميع الأسماك! جاري الانتقال لشاشة الفوز...");
        
        // هذا السطر يقوم بفتح السين الجديد بناءً على الاسم الذي ستحدده في الـ Inspector
        SceneManager.LoadScene(winSceneName);
    }
}