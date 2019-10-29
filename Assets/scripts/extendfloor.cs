using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extendfloor : MovingPlatform {

    public bool left;
    public bool open;
    public float doorheight;
    public float timer;
    public bool multiplebat;
    public int mbattype;
    public battery[] bats;
    // Use this for initialization
    void Start()
    {
        startpos = transform.position;
        if (left)
        {
            goalpos = startpos + new Vector2(doorheight,0);
        }
        else
        {
            goalpos = startpos + new Vector2(-doorheight,0);
        }
        vel = (goalpos - startpos).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    { 
    if (multiplebat)
        {
            if (mbattype == 0)
            {
                bool temp = true;
                for (int i = 0; i<bats.Length; i++)
                {
                    if (!bats[i].powered)
                    {
                        temp = false;
                    }
                }
                if (GetComponentInParent<battery>().powered && temp)
                {
                    open = true;
                }
                else
                {
                    open = false;
                }
            }
            if (mbattype == 1)
            {
                bool temp = false;
                for (int i = 0; i<bats.Length; i++)
                {
                    if (bats[i].powered)
                    {
                        temp = true;
                    }
                }
                if (GetComponentInParent<battery>().powered || temp)
                {
                    open = true;
                }
                else
                {
                    open = false;
                }
            }
            if (mbattype == 2)
            {
                if (GetComponentInParent<battery>().powered && !bats[0].powered)
                {
                    open = true;
                }
                else if (!GetComponentInParent<battery>().powered && bats[0].powered)
                {
                    open = true;
                }
                else
                {
                    open = false;
                }
             
            }
        }
        else
        {
            if (GetComponentInParent<battery>().powered)
            {
                open = true;
            }
            else
            {
                open = false;
            }
        }

        if(open && Vector2.Distance(transform.position,startpos) < Vector2.Distance(goalpos, startpos))
        {
            leaving = true;
            Move();
        }
        else if(!open && Vector2.Distance(transform.position, goalpos) < Vector2.Distance(goalpos, startpos))
        {
            leaving = false;
            Move();
        }
        else
        {
            stop();
        }
       
    }
}