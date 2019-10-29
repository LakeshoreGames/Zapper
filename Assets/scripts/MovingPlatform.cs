using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector2 startpos;
    public Vector2 goalpos;
    public Vector2 vel;
    public float speed;
    public Rigidbody2D self;
    public bool leaving;
    // Use this for initialization
    void Start () {
        startpos = transform.position;
        vel = (goalpos - startpos).normalized * speed;
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void Move()
    {
        if (leaving)
        {
            self.velocity = vel;
        }
        else
        {
            self.velocity = -vel;
        }
    }
    public void stop()
    {
        self.velocity = new Vector2(0,0);
    }
}
