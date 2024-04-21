using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CleaningTool : MonoBehaviour
{
    public GameObject Brush;
    public GameObject _bubbleParticles;
    public float _bubblespawnRate;
    public float _dustspawnRate;
    public GameObject _dustParticles;
    public int _winRate = 60;
    [SerializeField] private GameObject _mirror;
    [SerializeField] private GameObject _cleanLayer;
    [SerializeField] private RenderTexture _mirrorTexture;
    [SerializeField] private GameObject _mirrorGunk;
    [SerializeField] private GameObject _cleanLayerGunk;
    [SerializeField] private RenderTexture _mirrorTextureGunk;

    [SerializeField] private float _intervalTime;
    private readonly HashSet<Vector2> _brushPositions = new();
    private readonly HashSet<Vector2> _brushPositionsGunk = new();
    private Bounds _bounds;
    public float MirrorPercentage = 0f;
    public float MirrorGunkPercentage = 0f;

    public void Move(Vector3 position, bool sponge = false)
    {
       if (position.x > _bounds.max.x || position.x < _bounds.min.x || 
        position.y > _bounds.max.y || position.y < _bounds.min.y)
        {
            return;
        }
        Vector2 coordinates = new (position.x, position.y);

        if (sponge && !_brushPositionsGunk.Contains(coordinates))
        {
            Vector3 posToSpawn = new(position.x, position.y, 0f);
            Instantiate(Brush, posToSpawn, quaternion.identity, _cleanLayerGunk.transform);
            _brushPositionsGunk.Add(coordinates);
            if (UnityEngine.Random.Range(0, 100) <= _bubblespawnRate)
            {
                var bubbles =Instantiate(_bubbleParticles, posToSpawn, quaternion.identity, _cleanLayerGunk.transform);
                Destroy(bubbles, 1f);
            }
        }
        else if (!_brushPositions.Contains(coordinates))
        {
            Vector3 posToSpawn = new(position.x, position.y, 0f);
            Instantiate(Brush, posToSpawn, quaternion.identity, _cleanLayer.transform);
            _brushPositions.Add(coordinates);
            if (UnityEngine.Random.Range(0, 100) <= _dustspawnRate)
            {
                var bubbles = Instantiate(_dustParticles, posToSpawn, quaternion.identity, _cleanLayer.transform);
                Destroy(bubbles, 1f);
            }
        }
    
    }

    public void setBrush(GameObject brush){
        Brush = brush;
    }

    private void Start()
    {
        StartCoroutine(CheckCleanedArea());
        StartCoroutine(CheckCleanedGunkArea());
    }

    private IEnumerator CheckCleanedArea()
    {
        float whitePixels = 0;
        Texture2D texture = new(_mirrorTexture.width, _mirrorTexture.height);

        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = _mirrorTexture;

        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        RenderTexture.active = currentActiveRT;

        Color32[] colors = texture.GetPixels32();
        for (int i = 0; i < colors.Length; i ++)
        {
            if (colors[i].a > _winRate && colors[i].r > 200 && colors[i].g > 200 && colors[i].b > 200)
            {
                whitePixels++;
            }

            if (i % 1000 == 0) // Yield every 1000 pixels to avoid freezing
            {
                yield return null;
            }
        }
        MirrorPercentage = whitePixels / colors.Length * 100;
        if (whitePixels / colors.Length != 1)
        {
            StartCoroutine(CheckCleanedArea());
        }
    }

    private IEnumerator CheckCleanedGunkArea()
    {
        float whitePixels = 0;
        Texture2D texture = new(_mirrorTextureGunk.width, _mirrorTextureGunk.height);

        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = _mirrorTextureGunk;

        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        RenderTexture.active = currentActiveRT;

        Color32[] colors = texture.GetPixels32();
        for (int i = 0; i < colors.Length; i ++)
        {
            if (colors[i].a > 60 && colors[i].r > 200 && colors[i].g > 200 && colors[i].b > 200)
            {
                whitePixels++;
            }

            if (i % 1000 == 0) // Yield every 1000 pixels to avoid freezing
            {
                yield return null;
            }
        }
        MirrorGunkPercentage = whitePixels / colors.Length * 100;
        if (whitePixels / colors.Length != 1)
        {
            StartCoroutine(CheckCleanedArea());
        }
    }

    private void Awake()
    {
        _bounds = _mirror.GetComponent<SpriteRenderer>().bounds;
    }
}
