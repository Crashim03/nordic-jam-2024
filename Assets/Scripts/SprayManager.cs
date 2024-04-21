using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SprayManager : MonoBehaviour
{
    [SerializeField] private GameObject SprayPrefab;
    private bool _cooldown = false;

    public void Spray(Vector3 mousePosition) 
    {
        if (_cooldown)
        {
            return;
        }
        GameObject spray = Instantiate(SprayPrefab, mousePosition + new Vector3(0f, 0f, 5f), quaternion.identity, transform);
        StartCoroutine(Fade(spray.GetComponent<SpriteRenderer>()));
        StartCoroutine(Cooldown());
    }
    
    private IEnumerator Fade(SpriteRenderer spriteRenderer)
    {
        while (spriteRenderer.color.a > 0f)
        {
            Color color = spriteRenderer.color;
            color.a -= 0.01f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);
        }
        Destroy(spriteRenderer.gameObject);
    }

    private IEnumerator Cooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(3f);
        _cooldown = false;
    }
}