using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Club1");
    }
    public void Credits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
