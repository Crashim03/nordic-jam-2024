using UnityEngine;

[CreateAssetMenu(fileName = "ToolStats", menuName = "ScriptableObjects/ToolStats", order = 1)]
public class ToolStats : ScriptableObject
{
    public Sprite PickUpSprite;
    public Sprite ToolSprite;
    public Sprite ParticleSprite;
    public float Damage;
}