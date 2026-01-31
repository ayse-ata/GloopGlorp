using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Intro");
    }
    public void Credits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
