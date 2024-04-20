using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform targetTransform; // The target to follow

    // Update is called once per frame
    void Update()
    {
        if(targetTransform != null)
        {
            // Set the position of this object to match the position of the target
            transform.position = targetTransform.position;
        }
    }
}
