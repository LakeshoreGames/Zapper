using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : enemy {
    public Transform buldir;
    public GameObject bullet;
    public float bulletspeed;
    public float shottimer;
    public float curtime;
    public Sprite[] anim;
    public bool inanim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        curtime += Time.deltaTime;
        if(curtime >= shottimer)
        {
            curtime = 0;
            GameObject temp = Instantiate(bullet, buldir.position, buldir.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = (buldir.position - transform.position).normalized * bulletspeed;
            inanim = true;
            GetComponent<SpriteRenderer>().sprite = anim[1];
        }
        if (health <= 0)
        {
            Instantiate(deathanim, transform.position, transform.rotation);
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
            if (flashtimer >= 1)
            {
                red = false;
            }
        }
        if(inanim)
        {
            if(curtime>0.05 && curtime < 0.1)
            {
                GetComponent<SpriteRenderer>().sprite = anim[2];
            }
            if (curtime > 0.1 && curtime < 0.15)
            {
                GetComponent<SpriteRenderer>().sprite = anim[3];
            }
            if (curtime > 0.15 && curtime < 0.2)
            {
                GetComponent<SpriteRenderer>().sprite = anim[2];
            }
            if (curtime > 0.2 && curtime < 0.25)
            {
                GetComponent<SpriteRenderer>().sprite = anim[1];
            }
            if (curtime > 0.25 )
            {
                GetComponent<SpriteRenderer>().sprite = anim[0];
                inanim = false;
            }

        }
    }
}
