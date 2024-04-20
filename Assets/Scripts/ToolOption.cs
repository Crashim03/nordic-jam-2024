using System;
using System.Collections;
using UnityEngine;

public class ToolOption : MonoBehaviour
{
    [SerializeField] Tools tool;
    private bool taken;
    private bool isSoundPlaying = false;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] ToolLogic manager;
    [SerializeField] GameObject brush;
    [SerializeField] AudioClip[] useSound;

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            if (UnityEngine.Random.value < 0.05f) 
            {
                PlayUseSound();
            }
        }
    }

    private void PlayUseSound()
    {
        if (useSound != null && !isSoundPlaying)
        {
            isSoundPlaying = true;
            int rand = UnityEngine.Random.Range(0, useSound.Length);
            AudioSource.PlayClipAtPoint(useSound[rand], transform.position);
            StartCoroutine(ResetSoundFlag(useSound[rand].length));
        }
    }

    private IEnumerator ResetSoundFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSoundPlaying = false;
    }

    public void set(){
        PlayUseSound();
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
