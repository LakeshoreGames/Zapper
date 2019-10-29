using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingenemy : MonoBehaviour {
    public Vector3 goalpos;
    public Vector3 startpos;
    public bool leaving;
    public bool elevator;
    public bool on;
    public float timer;
    public float speed;

    // Use this for initialization
    void Start () {
        startpos = transform.position;
        if (speed == 0)
        {
            speed = 1;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(elevator)
        {
            if (GetComponentInParent<battery>().powered)
            {
                on = true;
            }
            else
            {
                on = false;
            }
        }
        else
        {
            on = true;
        }

        if (on)
        {
            if (leaving)
            {
                timer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startpos, goalpos, timer);
                if (timer >= 1)
                {
                    timer = 0;
                    leaving = false;
                }
            }
            else
            {
                timer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(goalpos, startpos, timer);
                if (timer >= 1)
                {
                    timer = 0;
                    leaving = true;
                }
            }
        }
    }
}
