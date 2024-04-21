using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class cursor : MonoBehaviour
{
    public GameObject customCursor;
    private Vector3 mousePosition;
    private List<Sticker> stickers = new List<Sticker>();
    private ToolOption tool;

    private Vector3 previousMousePosition;
    private bool sticked = false;
    private Sticker stickedSticker;
    
    private float unstickDistanceThreshold = 20f;

    [HideInInspector] public CleaningTool CurrentCleaningTool;
    [SerializeField] ToolLogic manager;
    [SerializeField] AudioClip stickerAudio;
    [SerializeField] private SprayManager _sprayManager;
    private bool _spraying = false;

    private void Awake()
    {
        CurrentCleaningTool = GetComponent<CleaningTool>();
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        customCursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        
        Vector3 currentVelocity = (mousePosition - previousMousePosition) / Time.deltaTime;
        float speed = currentVelocity.magnitude;

        

        if (CurrentCleaningTool != null && Input.GetMouseButton(0) && (manager.tool == Tools.PANO || manager.tool == Tools.TOOL || manager.tool == Tools.SPONGE))
        {
            CurrentCleaningTool.setBrush(manager.brush);
            CurrentCleaningTool.Move(mousePosition, manager.tool == Tools.SPONGE);
        }
        else if (CurrentCleaningTool != null && Input.GetMouseButtonDown(0) && manager.tool == Tools.SPRAY)
        {
            _sprayManager.Spray(mousePosition);
        }

        // Sticker Detection and Logic
        if (manager.tool == Tools.HAND)
        {
            if (Input.GetMouseButtonDown(0) && stickers.Count > 0)
            {
                stickedSticker = stickers[0];
                sticked = true;
            }  
            else if (Input.GetMouseButton(0) && stickers.Count == 0 && sticked)
            {
                stickedSticker.pull();
                AudioSource.PlayClipAtPoint(stickerAudio, transform.position);
                sticked = false;
            }
        }

        if (!Input.GetMouseButton(0) || manager.tool != Tools.HAND)
        {
            sticked = false;
        }
        
        if (Input.GetMouseButtonDown(0)) //Tool Selection
        {
            checkForTool();
        }

        previousMousePosition = mousePosition; 
    }

    private void Instatiate(GameObject spray, Vector3 mousePosition, quaternion identity, Transform transform)
    {
        throw new NotImplementedException();
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
        if (other.CompareTag("Spray") && manager.tool != Tools.SPRAY && manager.tool != Tools.HAND)
        {
            StartCoroutine(SprayBrush());
        }
    }

    private IEnumerator SprayBrush()
    {
        if (_spraying == false)
        {
            Debug.Log("Spraying");
            _spraying = true;
            GameObject initialCleaningTool = CurrentCleaningTool.Brush;
            Vector3 initialScale = initialCleaningTool.transform.localScale;
            CurrentCleaningTool.Brush.transform.localScale = initialScale * 1.3f;
            Debug.Log(CurrentCleaningTool.Brush.transform.localScale);
            yield return new WaitForSeconds(1f);
            CurrentCleaningTool.Brush.transform.localScale = initialScale;
            Debug.Log("Stopping");
            Debug.Log(CurrentCleaningTool.Brush.transform.localScale);
            _spraying = false;
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
