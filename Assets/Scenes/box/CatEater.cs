using UnityEngine;
using TMPro; 

public class CatEater : MonoBehaviour
{ 
    private Animator anim;
    private int ballCount = 0;

    [Header("UI Settings")]
    // خانة سحب نص الرقم في الـ Inspector
    [SerializeField] private TMP_Text scoreText; 

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource; // خانة سحب مكون الصوت (AudioSource)
    [SerializeField] private AudioClip meowSound;     // خانة سحب ملف صوت المواء (AudioClip)

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateScoreUI();

        // فحص تلقائي: إذا نسيت سحب الـ AudioSource، يحاول الكود إيجاده بنفسه على نفس الكائن
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            anim.SetTrigger("Eat");
            ballCount++;
            UpdateScoreUI();
            
            // استدعاء دالة تشغيل الصوت عند الأكل
            PlayMeowSound();

            Destroy(other.gameObject);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            // يعرض الرقم فقط
            scoreText.text = ballCount.ToString(); 
        }
    }

    void PlayMeowSound()
    {
        // التأكد من أن الخانات ممتلئة بالـ Inspector قبل التشغيل لتجنب الأخطاء (Errors)
        if (audioSource != null && meowSound != null)
        {
            // تشغيل الصوت مرة واحدة بدون تداخل
            audioSource.PlayOneShot(meowSound);
        }
    }
}