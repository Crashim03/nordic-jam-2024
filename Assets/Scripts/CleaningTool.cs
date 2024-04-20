using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class CleaningTool : MonoBehaviour
{
    public GameObject Brush;
    [SerializeField] private GameObject _mirror;
    [SerializeField] private GameObject _cleanLayer;
    [SerializeField] private float _interval = 0.5f;
    private readonly HashSet<Vector2> _brushPositions = new();
    private readonly List<Vector2> _toClean = new();
    private Bounds _bounds;
    private float _brushRadius;
    private float _toCleanCount;

    public void Move(Vector3 position)
    {
       if (position.x > _bounds.max.x || position.x < _bounds.min.x || 
        position.y > _bounds.max.y || position.y < _bounds.min.y)
        {
            return;
        }
        Vector2 coordinates = new (position.x, position.y);

        if (!_brushPositions.Contains(coordinates))
        {
            Instantiate(Brush, position, quaternion.identity, _cleanLayer.transform);
            _brushPositions.Add(coordinates);
        }
        foreach (Vector2 coordinate in new List<Vector2>(_toClean))
        {
            if (Vector2.Distance(coordinate, position) <= _brushRadius)
            {
                _toClean.Remove(coordinate);
            }
        }
        Debug.Log(_toClean.Count / _toCleanCount * 100);
    }

    private void Start()
    {
        for (float i = _bounds.min.x; i < _bounds.max.x; i += _interval)
        {
            for (float j = _bounds.min.y; j < _bounds.max.y; j += _interval)
            {
                _toClean.Add(new Vector2(i, j));
            }
        }
        _toCleanCount = _toClean.Count;
        Debug.Log(_toCleanCount);
    }

    private void Awake()
    {
        _bounds = _mirror.GetComponent<Collider2D>().bounds;
        _brushRadius = Brush.GetComponent<SpriteMask>().bounds.size.x / 2;
    }
}
