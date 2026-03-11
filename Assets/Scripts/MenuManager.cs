using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TeikaGame");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Does Not Quit in editor window");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
