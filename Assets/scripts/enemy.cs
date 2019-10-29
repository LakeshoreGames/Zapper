using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    public float health;
    public float flashtimer;
    public bool red;
    public GameObject deathanim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health <=0)
        {
            Instantiate(deathanim,transform.position,transform.rotation);
            Destroy(gameObject);
        }
        if (red)
        {
            flashtimer += Time.deltaTime;
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.r = 1;
            temp.b = flashtimer;
            temp.g = flashtimer;
            GetComponent<SpriteRenderer>().color = temp;
            if(flashtimer >=1)
            {
                red = false;
            }
        }
	}

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("playerattack"))
        {
            health -= Time.deltaTime*2;
            if (!red)
            {
                red = true;
                flashtimer = 0;
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.r = 1;
                temp.b = 0;
                temp.g = 0;
                GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }
}
