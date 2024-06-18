using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Paw _paw;
    private Tool _currentTool;
    private List<ToolPickup> _toolsHovered = new(); 
    private bool _click = false;

    private void Start()
    {
        _currentTool = _paw;
    }

    private void PickUpTool(Tool tool)
    {
        _currentTool = tool;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        _currentTool.Move(mousePosition);
        transform.position = mousePosition;

        bool newClick = Input.GetMouseButton(0);

        if (!_click && newClick)
        {
            _click = true;
            Click();
        }
        else
        {
            _click = newClick;
        }
    }

    private void Click()
    {
        if (_toolsHovered.Count == 0)
        {
            _currentTool.Action();
            return;
        }

        ToolPickup closestTool = _toolsHovered[0];

        foreach (ToolPickup tool in _toolsHovered)
        {
            if (Vector3.Distance(tool.transform.position, transform.position) < Vector3.Distance(closestTool.transform.position, transform.position))
            {
                closestTool = tool;
            }
        }

        if (closestTool.Tool != null)
        {
            _currentTool = closestTool.PickUp();
        }
        else if (_currentTool != _paw)
        {
            closestTool.Place(_currentTool);
            _currentTool = _paw;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other);
        if (other.gameObject.TryGetComponent(out ToolPickup pickUp))
        {
            _toolsHovered.Add(pickUp);
        };
    }

       private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ToolPickup pickUp))
        {
            _toolsHovered.Remove(pickUp);
        };
    }


//     public GameObject customCursor;
//     private Vector3 mousePosition;
//     private List<Sticker> stickers = new List<Sticker>();
//     private ToolOption tool;

//     private Vector3 previousMousePosition;
//     private bool sticked = false;
//     private Sticker stickedSticker;
    
//     private float unstickDistanceThreshold = 20f;

//     [HideInInspector] public CleaningTool CurrentCleaningTool;
//     [SerializeField] ToolLogic manager;
//     [SerializeField] AudioClip stickerAudio;
//     [SerializeField] private SprayManager _sprayManager;
//     private bool _spraying = false;

//     private void Awake()
//     {
//         CurrentCleaningTool = GetComponent<CleaningTool>();
//         UnityEngine.Cursor.visible = false;
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             SceneManager.LoadScene(0);
//         } 

//         mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         customCursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        
//         Vector3 currentVelocity = (mousePosition - previousMousePosition) / Time.deltaTime;
//         float speed = currentVelocity.magnitude;

        

//         if (CurrentCleaningTool != null && Input.GetMouseButton(0) && (manager.tool == Tools.PANO || manager.tool == Tools.TOOL || manager.tool == Tools.SPONGE))
//         {
//             CurrentCleaningTool.setBrush(manager.brush);
//             CurrentCleaningTool.Move(mousePosition, manager.tool == Tools.SPONGE);
//         }
//         else if (CurrentCleaningTool != null && Input.GetMouseButtonDown(0) && manager.tool == Tools.SPRAY)
//         {
//             _sprayManager.Spray(mousePosition);
//         }

//         // Sticker Detection and Logic
//         if (manager.tool == Tools.HAND)
//         {
//             if (Input.GetMouseButtonDown(0) && stickers.Count > 0)
//             {
//                 stickedSticker = stickers[0];
//                 sticked = true;
//             }  
//             else if (Input.GetMouseButton(0) && stickers.Count == 0 && sticked)
//             {
//                 stickedSticker.pull();
//                 AudioSource.PlayClipAtPoint(stickerAudio, transform.position);
//                 sticked = false;
//             }
//         }

//         if (!Input.GetMouseButton(0) || manager.tool != Tools.HAND)
//         {
//             sticked = false;
//         }
        
//         if (Input.GetMouseButtonDown(0)) //Tool Selection
//         {
//             checkForTool();
//         }

//         previousMousePosition = mousePosition; 
//     }

//     private void Instatiate(GameObject spray, Vector3 mousePosition, quaternion identity, Transform transform)
//     {
//         throw new NotImplementedException();
//     }

//     private void checkForTool()
//     {
//         if (tool != null)
//         {
//             tool.set();
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         Sticker sticker = other.gameObject.GetComponent<Sticker>();
//         if (sticker != null && !stickers.Contains(sticker))
//         {
//             stickers.Add(sticker);
//         }
//         ToolOption newTool = other.gameObject.GetComponent<ToolOption>();
//         if (newTool != null)
//         {
//             tool = newTool;
//         }
//         if (other.CompareTag("Spray") && manager.tool != Tools.SPRAY && manager.tool != Tools.HAND)
//         {
//             StartCoroutine(SprayBrush());
//         }
//     }

//     private IEnumerator SprayBrush()
//     {
//         if (_spraying == false)
//         {
//             Debug.Log("Spraying");
//             _spraying = true;
//             GameObject initialCleaningTool = CurrentCleaningTool.Brush;
//             Vector3 initialScale = initialCleaningTool.transform.localScale;
//             CurrentCleaningTool.Brush.transform.localScale = initialScale * 1.3f;
//             Debug.Log(CurrentCleaningTool.Brush.transform.localScale);
//             yield return new WaitForSeconds(1f);
//             CurrentCleaningTool.Brush.transform.localScale = initialScale;
//             Debug.Log("Stopping");
//             Debug.Log(CurrentCleaningTool.Brush.transform.localScale);
//             _spraying = false;
//         }
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         Sticker sticker = other.gameObject.GetComponent<Sticker>();
//         if (sticker != null && stickers.Contains(sticker))
//         {
//             stickers.Remove(sticker);
//             StopCoroutine(UnstickAfterDelay(.5f));
//         }
//         ToolOption newTool = other.gameObject.GetComponent<ToolOption>();
//         if (newTool != null && tool == newTool)
//         {
//             tool = null;
//         }
//     }

//     IEnumerator UnstickAfterDelay(float stickedDuration)
//     {
//         yield return new WaitForSeconds(stickedDuration);
//         sticked = false;
//     }
}
