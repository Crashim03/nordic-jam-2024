using System;
using UnityEngine;

public class ToolOption : MonoBehaviour
{
    [SerializeField] Tools tool;
    private bool taken;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] ToolLogic manager;
    [SerializeField] GameObject brush;

    
    public void set(){
        Color spriteColor = sprite.color;
        if (taken) 
        { 
            manager.SetTool(Tools.HAND); 
            spriteColor.a = 1;
        }
        else 
        { 
            resetOthers();
            manager.SetTool(tool); 
            manager.brush = brush;
            spriteColor.a = 0;
        }
        taken = !taken;
        sprite.color = spriteColor;
        
    }

    public void resetOthers()
    {
        ToolOption[] allToolOptions = FindObjectsOfType<ToolOption>();

        foreach (ToolOption option in allToolOptions)
        {
            if (option != this)
                option.freeHand();
        }
    }

    private void freeHand()
    {
        Color spriteColor = sprite.color;
        spriteColor.a = 1;
        taken = false;
        sprite.color = spriteColor;
    }
}
