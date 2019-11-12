using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableplatform : MovingPlatform {
    public bool on;
    public bool nonline;
    public bool loop;
    public bool fin;
    public int destnum;

    public Vector2[] dests;
    // Use this for initialization
    void Start () {
        if (nonline)
        {
            fin = false;
            leaving = true;
            goalpos = dests[1];
            startpos = dests[0];
            destnum = 1;
            vel = (goalpos - startpos).normalized * speed;
        }
        else
        {
            startpos = transform.position;
            vel = (goalpos - startpos).normalized * speed;
            leaving = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponentInParent<battery>() != null)
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
            if (GetComponentInChildren<battery>().powered)
            {
                on = true;
            }
            else
            {
                on = false;
            }

        }

        if (on && !fin)
        {
            Move();
        }
        else 
        {
            stop();
        }

        if (nonline && !fin)
        {
            if (Vector2.Distance(transform.position, startpos) >= Vector2.Distance(goalpos, startpos))
            {
                startpos = dests[destnum];
                destnum++;
                if (destnum == dests.Length)
                {
                    if (loop)
                    {
                        destnum = 0;
                        goalpos = dests[destnum];
                        vel = (goalpos - startpos).normalized * speed;
                    }
                    else
                    {
                        fin = true;
                    }
                }
                else
                {
                    goalpos = dests[destnum];
                    vel = (goalpos - startpos).normalized * speed;
                }
          
            }
        }
        else
        {
            if (leaving && Vector2.Distance(transform.position, startpos) >= Vector2.Distance(goalpos, startpos))
            {
                leaving = false;
            }
            if (!leaving && Vector2.Distance(transform.position, goalpos) >= Vector2.Distance(goalpos, startpos))
            {
                leaving = true;
            }
        }
    }
}
