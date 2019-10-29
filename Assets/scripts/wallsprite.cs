using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsprite : MonoBehaviour {
    public Sprite[] sprites;
    public int spritenum;
    // Use this for initialization
    [ExecuteInEditMode]

    void Start () {
        GetComponent<SpriteRenderer>().sprite = sprites[spritenum];
        if(spritenum == 7 || spritenum == 9 || spritenum == 8 || spritenum == 11 || spritenum == 13 || spritenum == 14 || spritenum == 10 || spritenum == 12 )
        {
            Vector2 temp = new Vector2(0.32f, 0.32f);
            GetComponent<BoxCollider2D>().size  = temp;
        }
	}

}
