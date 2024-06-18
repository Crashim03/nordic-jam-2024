using System;
using UnityEngine;
using UnityEngine.Events;

public class DirtyArea : MonoBehaviour
{
    private float _health = 100f;
    private SpriteRenderer _spriteRenderer;
    private Mirror _mirror;

    public void LooseHealth(float damage)
    {
        _health -= damage;

        _mirror.CleanArea(Math.Clamp(damage, 0f, _health));

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