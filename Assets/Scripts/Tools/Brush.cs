using System.Collections.Generic;
using UnityEngine;

public class Brush : Tool 
{
    private List<GameObject> _dirtHovered = new(); 
    public override void Action()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _dirtHovered.Add(other.gameObject);
    }

       private void OnTriggerExit2D(Collider2D other)
    {
        _dirtHovered.Remove(other.gameObject);
    }
}