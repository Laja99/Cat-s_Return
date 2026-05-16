using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // 💡 New function to return to the Main Menu (Scene 0)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}