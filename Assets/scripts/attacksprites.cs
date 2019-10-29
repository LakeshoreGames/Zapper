using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacksprites : MonoBehaviour {
    public Sprite[] sprites;
    public float timer;
    public float curtime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        curtime += Time.deltaTime;
        if(curtime > timer)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 4)];
            curtime = 0;
        }
	}
}
