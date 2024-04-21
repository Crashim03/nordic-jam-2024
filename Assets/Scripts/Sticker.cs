using UnityEngine;

public class Sticker : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] Sprite[] sprites;
    int id = 0;

    void Update()
    {
        if(hp == 0){ Destroy(gameObject); }
        if (id < 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[id];
        }
    }

    public void pull(){
        hp--;
        id++;
    }
}
