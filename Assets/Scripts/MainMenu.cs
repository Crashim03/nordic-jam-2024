using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
