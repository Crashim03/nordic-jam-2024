using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools 
{
    HAND = 0,
    PANO = 1,
    SPRAY = 2,
    TOOL = 3,
    SPONGE = 4,
}

public class ToolLogic : MonoBehaviour
{
    [SerializeField] public Tools tool;
    [SerializeField] public GameObject brush;
    [SerializeField] public GameObject Spray;
    [SerializeField] public GameObject currentAudio;

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
