using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        
        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        
        // Calculate the direction from the eye to the mouse position
        Vector3 direction = (worldMousePosition - transform.position).normalized;
        
        // Calculate the angle of rotation needed to look at the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Apply the rotation to the eye
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
