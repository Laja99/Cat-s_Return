using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 
using System.Collections; 

public class CatEater : MonoBehaviour
{ 
    private Animator anim;
    private int ballCount = 0;
    private bool isWon = false; 
    private bool isGameOver = false;

    [Header("UI Settings")]
    [SerializeField] private TMP_Text scoreText; 
    // 💡 Slot for the countdown timer text
    [SerializeField] private TMP_Text timerText; 

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip meowSound;     
    [SerializeField] private AudioClip winSound;     

    [Header("Scene Settings")]
    [SerializeField] private int winSceneIndex = 3; 
    // 💡 Build Index slot for the Lose Scene
    [SerializeField] private int loseSceneIndex = 4; 

    [Header("Timer Settings")]
    [SerializeField] private float timeRemaining = 15f; 

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateScoreUI();
        UpdateTimerUI();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Stop the timer if the player already won or if time ran out
        if (isWon || isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            LoseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isWon || isGameOver) return;

        if (other.CompareTag("Ball"))
        {
            if (anim != null)
            {
                anim.SetTrigger("Eat");
            }

            ballCount++;
            UpdateScoreUI();
            PlayMeowSound();
            Destroy(other.gameObject);

            if (ballCount >= 4)
            {
                WinGame();
            }
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = ballCount.ToString(); 
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Displays time rounded to the nearest whole second
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString(); 
        }
    }

    void PlayMeowSound()
    {
        if (audioSource != null && meowSound != null)
        {
            audioSource.PlayOneShot(meowSound);
        }
    }

    void WinGame()
    {
        isWon = true; 
        Debug.Log("Game Won! Starting Win Sequence...");
        StartCoroutine(PlayWinSoundAndLoadScene());
    }

    void LoseGame()
    {
        isGameOver = true;
        Debug.Log("Time is up! Loading Lose Scene...");
        SceneManager.LoadScene(loseSceneIndex);
    }

    IEnumerator PlayWinSoundAndLoadScene()
    {
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(winSceneIndex);
    }
}