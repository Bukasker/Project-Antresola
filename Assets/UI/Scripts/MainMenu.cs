using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
