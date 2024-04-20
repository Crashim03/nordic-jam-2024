using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] Sprite[] sprites;

    public void pull(){
        print(hp);
        hp--;
    }

    private void Update()
    {
        if(hp == 0){ Destroy(gameObject); }
    }
}
