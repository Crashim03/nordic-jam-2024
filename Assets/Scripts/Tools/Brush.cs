using System;
using System.Collections.Generic;
using UnityEngine;

public class Brush : Tool 
{
    private readonly List<DirtyArea> _dirtHovered = new(); 
    public override void Click()
    {
        Clean();
    }

    public override void Hold()
    {
        Clean();
    }

    private void Clean()
    {
        var dirtHoveredCopy = new List<DirtyArea>(_dirtHovered);
        dirtHoveredCopy.ForEach(dirt => {
            dirt.LooseHealth(ToolStats.Damage);
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DirtyArea area))
        {
            _dirtHovered.Add(area);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out DirtyArea area))
        {
            _dirtHovered.Remove(area);
        }
    }
}