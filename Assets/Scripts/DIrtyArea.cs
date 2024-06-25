using System;
using UnityEngine;
using UnityEngine.Events;

public class DirtyArea : MonoBehaviour
{
    public Mirror Mirror;
    private float _health = 100f;
    private SpriteRenderer _spriteRenderer;

    public void LooseHealth(float damage)
    {
        Mirror.CleanArea(Math.Clamp(damage, 0f, _health));
        _health -= damage;

        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _health / 100f);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}