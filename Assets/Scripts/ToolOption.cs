using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToolOption : MonoBehaviour
{
    [SerializeField] Tools tool;
    private bool taken;
    [SerializeField] Image sprite;
    [SerializeField] ToolLogic manager;
    
    public void set(){
        Color spriteColor = sprite.color;
        if (taken) 
        { 
            manager.SetTool(Tools.HAND); 
            spriteColor.a = 1;
        }
        else 
        { 
            manager.SetTool(tool); 
            spriteColor.a = 0;
        }
        taken = !taken;
        sprite.color = spriteColor;
        
    }
}
