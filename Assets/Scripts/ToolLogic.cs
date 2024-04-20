using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools 
{
    HAND = 0,
    PANO = 1,
    SPRAY = 2,
}

public class ToolLogic : MonoBehaviour
{
    [SerializeField] Tools tool;

    [SerializeField] GameObject[] hands;

    public void SetTool(Tools newTool)
    {
        hands[(int)tool].SetActive(false);
        tool = newTool;
        hands[(int)newTool].SetActive(true);
        print(newTool);
    }

    public void useTool()
    {
        print("whoosh");
    }
}
