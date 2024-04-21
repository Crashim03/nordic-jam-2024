using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningState: MonoBehaviour
{
    public float MinCleanMirror = 95f;
    public float MinCleanGunk = 95f;
    public int StickersToBeRemoved = 3;
    public int CurrentStickers = 0;
    public CleaningTool cleaningTool;

    private void Update()
    {
        if (cleaningTool.MirrorGunkPercentage >= MinCleanGunk && cleaningTool.MirrorPercentage >= MinCleanMirror && CurrentStickers == StickersToBeRemoved)
        {
            Win();
        }
    }

    public void Win()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}