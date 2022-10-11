using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnQuit() 
    {
        Application.Quit();
    }

    public void OnSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayerScene");
    }

    public void OnMultiPlayer()
    {
        SceneManager.LoadScene("MultiPlayerScene");
    }
    
    public void OnOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}
