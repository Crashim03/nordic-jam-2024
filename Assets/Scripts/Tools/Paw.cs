using System.Collections.Generic;
using UnityEngine;

public class Paw : Tool 
{
    private List<GameObject> _stickersHovered = new(); 

    public override void Action()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _stickersHovered.Add(other.gameObject);
    }

       private void OnTriggerExit2D(Collider2D other)
    {
        _stickersHovered.Remove(other.gameObject);
    }
}