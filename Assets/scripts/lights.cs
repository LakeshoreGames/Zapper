using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour {

    public float timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<battery>().powered)
        {
                timer += Time.deltaTime;
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.a = 1-timer;
                GetComponent<SpriteRenderer>().color = temp;
                if(timer > 1)
                {
                timer = 1;
                }
            
        }
        else
        {
                timer -= Time.deltaTime;
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.a = 1- timer;
                GetComponent<SpriteRenderer>().color = temp;
                if (timer <= 0)
                {
                    timer = 0;
                }
            
        }
       
    }
}
