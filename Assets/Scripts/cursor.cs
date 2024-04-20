using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class cursor : MonoBehaviour
{
    public GameObject customCursor;
    private Vector3 mousePosition;
    private List<Sticker> stickers = new List<Sticker>();
    private ToolOption tool;

    private Vector3 previousMousePosition;
    private float flickThreshold = 70f;
    private bool sticked = false;
    private Sticker stickedSticker;
    private float unstickDistanceThreshold = 20f;


    void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        customCursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

        if (Input.GetMouseButtonDown(0) && stickers.Count > 0)
        {
            stickedSticker = stickers[0];
            sticked = true;
        }  
        else if (Input.GetMouseButton(0) && stickers.Count > 0 && sticked)
        {
            Vector3 currentVelocity = (mousePosition - previousMousePosition) / Time.deltaTime;
            float speed = currentVelocity.magnitude;
            if (speed > flickThreshold)
            {
                stickedSticker.pull();
                sticked = false;
            }
        }
        else if (!Input.GetMouseButton(0))
        {
            sticked = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            checkForTool();
        }

        previousMousePosition = mousePosition; 
    }

    private void checkForTool()
    {
        if (tool != null)
        {
            tool.set();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Sticker sticker = other.gameObject.GetComponent<Sticker>();
        if (sticker != null && !stickers.Contains(sticker))
        {
            stickers.Add(sticker);
        }
        ToolOption newTool = other.gameObject.GetComponent<ToolOption>();
        if (newTool != null)
        {
            tool = newTool;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Sticker sticker = other.gameObject.GetComponent<Sticker>();
        if (sticker != null && stickers.Contains(sticker))
        {
            stickers.Remove(sticker);
            StopCoroutine(UnstickAfterDelay(.5f));
        }
        ToolOption newTool = other.gameObject.GetComponent<ToolOption>();
        if (newTool != null && tool == newTool)
        {
            tool = null;
        }
    }

    IEnumerator UnstickAfterDelay(float stickedDuration)
    {
        yield return new WaitForSeconds(stickedDuration);
        sticked = false;
    }
}
