using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laval : MonoBehaviour {
    public float curtime;
    public Sprite[] anim;
    public int animnum;
    public float interval;
    // Use this for initialization
    void Start () {
        animnum = 0;
	}
	
	// Update is called once per frame
	void Update () {
        curtime += Time.deltaTime;
	if(curtime < interval && animnum == 2)
        {
            GetComponent<SpriteRenderer>().sprite = anim[0];
            animnum = 0;
        }
        else if (curtime > interval && curtime < (2*interval) && animnum == 0)
        {
            GetComponent<SpriteRenderer>().sprite = anim[1];
            animnum = 1;
        }
        else if (curtime > (2 * interval) && curtime < (3 * interval) && animnum == 1)
        {
            GetComponent<SpriteRenderer>().sprite = anim[2];
            animnum = 2;
        }
        else if(curtime> (3 * interval))
        {
            curtime = 0;
        }
    }
}
