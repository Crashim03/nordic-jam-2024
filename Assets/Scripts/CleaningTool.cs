using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class CleaningTool : MonoBehaviour
{
    public GameObject Brush;
    [SerializeField] private GameObject _cleanLayer;
    private readonly HashSet<Vector2> positions = new();

    public void Move(Vector3 position)
    {
        Vector2 pos2D = new (position.x, position.y);

        if (!positions.Contains(pos2D))
        {
            Instantiate(Brush, position, quaternion.identity, _cleanLayer.transform);
            positions.Add(pos2D);
        }
        else
        {
            Debug.Log("Already there!");
        }
    }
}
