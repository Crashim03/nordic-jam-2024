using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.Cursor.visible = true;
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
