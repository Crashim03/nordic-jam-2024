using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public ToolStats ToolStats;
    public abstract void Click();
    public abstract void Hold();

    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}