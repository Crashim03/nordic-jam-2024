using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    public Tool Tool;
    private SpriteRenderer _spriteRenderer;

    public Tool PickUp()
    {
        Tool currentTool = Tool;
        Tool = null;
        _spriteRenderer.sprite = null;
        return currentTool;
    }

    public void Place(Tool tool)
    {
        Tool = tool;
        _spriteRenderer.sprite = Tool.ToolStats.PickUpSprite;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (Tool != null)
        {
            _spriteRenderer.sprite = Tool.ToolStats.PickUpSprite;
        }
    }
}