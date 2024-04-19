using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public GameObject customCursor;
    private Vector3 mousePosition;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        customCursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
        }
    }
}
