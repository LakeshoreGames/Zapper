using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battery : MonoBehaviour {

    public bool powered;
    public float fullpower;
    public float curpower;
    public bool beingpow;
    public float timer;
    public GameObject[] display;
    public AudioSource powerup;
    public AudioSource powerdown;
    public int type;
    public bool shell;

    // Use this for initialization
    void Start () {
        curpower = 0;
	}
	
	// Update is called once per frame
	void Update () {
       if(type == 0)
       {
            type0();
       }
       else if(type == 1)
       {
            type1();
       }
       else if(type == 2)
       {
            type2();
       }

	}
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("playerattack"))
        {
            if (type == 0)
            {
                if (curpower == 0)
                {
                    powerup.Play();
                }
                curpower += fullpower * 0.5f * Time.deltaTime;
                if (curpower > fullpower)
                {
                    curpower = fullpower;
                }
                beingpow = true;
            }        
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerattack"))
        {
            if (type == 1 )
            {
                if (powered == false)
                {
                    powered = true;
                    powerup.Play();
                }
                else
                {
                    powered = false;
                    powerdown.Play();
                }
            }
            if (type == 2)
            {
                powered = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("playerattack"))
        {
            if (type == 2)
            {
                powered = false ;
            }
        }
    }

    public void type0()
    {
        if (!beingpow)
        {
            if (curpower > 0)
            {
                powered = true;
                curpower -= Time.deltaTime;
                if (curpower <= 0)
                {
                    powerdown.Play();
                }
            }
            else
            {
                curpower = 0;
                powered = false;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                beingpow = false;
                timer = 0;
            }
        }

        float powerper = curpower / fullpower;
        if (!shell)
        {
            if (powerper < 0.2f)
            {
                Color temp = display[0].GetComponent<SpriteRenderer>().color;
                temp.r = 1 - powerper * 5;
                temp.g = powerper * 5;
                temp.b = 0;
                display[0].GetComponent<SpriteRenderer>().color = temp;

            }
            else if (powerper > 0.2f && powerper < 0.4f)
            {
                Color temp = display[1].GetComponent<SpriteRenderer>().color;
                temp.r = 1 - (powerper - 0.2f) * 5;
                temp.g = (powerper - 0.2f) * 5;
                temp.b = 0;
                display[1].GetComponent<SpriteRenderer>().color = temp;
            }
            else if (powerper > 0.4f && powerper < 0.6f)
            {
                Color temp = display[2].GetComponent<SpriteRenderer>().color;
                temp.r = 1 - (powerper - 0.4f) * 5;
                temp.g = (powerper - 0.4f) * 5;
                temp.b = 0;
                display[2].GetComponent<SpriteRenderer>().color = temp;
            }
            else if (powerper > 0.6f && powerper < 0.8f)
            {
                Color temp = display[3].GetComponent<SpriteRenderer>().color;
                temp.r = 1 - (powerper - 0.6f) * 5;
                temp.g = (powerper - 0.6f) * 5;
                temp.b = 0;
                display[3].GetComponent<SpriteRenderer>().color = temp;
            }
            else if (curpower / fullpower > 0.8f)
            {
                Color temp = display[4].GetComponent<SpriteRenderer>().color;
                temp.r = 1 - (powerper - 0.8f) * 5;
                temp.g = (powerper - 0.8f) * 5;
                temp.b = 0;
                display[4].GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }

    public void type1()
    {
        if (!shell)
        {
            if (powered)
            {
                Color temp = display[0].GetComponent<SpriteRenderer>().color;
                temp.r = 1;
                temp.g = 1;
                temp.b = 1;
                display[0].GetComponent<SpriteRenderer>().color = temp;

                temp = display[1].GetComponent<SpriteRenderer>().color;
                temp.r = 0;
                temp.g = 1;
                temp.b = 0;
                display[1].GetComponent<SpriteRenderer>().color = temp;

            }
            else
            {
                Color temp = display[0].GetComponent<SpriteRenderer>().color;
                temp.r = 1;
                temp.g = 0;
                temp.b = 0;
                display[0].GetComponent<SpriteRenderer>().color = temp;

                temp = display[1].GetComponent<SpriteRenderer>().color;
                temp.r = 1;
                temp.g = 1;
                temp.b = 1;
                display[1].GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }

    public void type2()
    {
        if (!shell)
        {
            if (powered)
            {
                Color temp = display[0].GetComponent<SpriteRenderer>().color;
                temp.r = 0;
                temp.g = 1;
                temp.b = 0;
                display[0].GetComponent<SpriteRenderer>().color = temp;

            }
            else
            {
                Color temp = display[0].GetComponent<SpriteRenderer>().color;
                temp.r = 1;
                temp.g = 0;
                temp.b = 0;
                display[0].GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }

}
