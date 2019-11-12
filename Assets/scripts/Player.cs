using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float attacktimer;
    public bool attackCD;
    public int health;
    public float speed;
    public bool grounded;
    public bool onmoveground;
    public bool hitmoveground;
    public bool jumping;
    public float grav;
    public Transform groundcheck;
    public Transform rightcheck;

    public int shoothor;
    public int shootvert;
    public GameObject attack;

    public Sprite idle;
    public Sprite moving1;
    public Sprite moving2;
    public float jumptimer;

    public Image[] hpbar;

    public float movetime;
    public bool reversed;
    public bool paused;
    public GameObject arm;
    public GameObject deathanim;

    public AudioSource jumpsfx;
    public AudioSource lasersfx;
    public AudioSource hitsfx;

    public bool invuln;
    public float invulntimer;
    public bool flash1;
    // Use this for initialization
    void Start () {
        speed = 2;
        health = 3;
        attackCD = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape") && !paused)
        {
            paused = true;
            Time.timeScale = 0;
            lasersfx.Stop();
            attack.SetActive(false);
            Manager.instance.pause_game();
        }
        else if (Input.GetKeyDown("escape") && paused)
        {
            paused = false;
            Time.timeScale = 1;
            Manager.instance.unpause_game();

        }

        if (!paused)
        {
            if (health <= 0)
            {
                Manager.instance.deathtimer = 1f;
                Instantiate(deathanim, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (invuln)
            {
                afterHitInvuln();
            }

            horizontal();
            mousestuff();
            vertical();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            if (!invuln)
            {
                hitsfx.Play();
                flash1 = false;
                health--;
                invuln = true;
                invulntimer = 1;
                Color temp = hpbar[health].color;
                temp.a = 0;
                hpbar[health].color = temp;
            }
        }
       
        if (collision.collider.CompareTag("exit"))
        {
            Manager.instance.finished();
        }
        if(collision.collider.CompareTag("moveground"))
        {
            hitmoveground = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("death"))
        {
            health = 0;
        }
    }

    void afterHitInvuln()
    {       
            invulntimer -= Time.deltaTime;
            if (invulntimer <= 0)
            {
                invuln = false;
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.a = 1;
                GetComponent<SpriteRenderer>().color = temp;
            }
            else
            {
                Color temp = GetComponent<SpriteRenderer>().color;
                if (!flash1)
                {
                    temp.a -= Time.deltaTime * 4;
                    if (temp.a < 0.5f)
                    {
                        flash1 = true;
                    }
                }
                else
                {
                    temp.a += Time.deltaTime * 4;
                    if (temp.a >= 1)
                    {
                        flash1 = false;
                    }
                }
                GetComponent<SpriteRenderer>().color = temp;
            }
        }

    void mousestuff()
    {
        Vector2 mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float attackangle = Mathf.Atan2(mospos.y - transform.position.y, mospos.x - transform.position.x) * Mathf.Rad2Deg;
        if (!reversed)
        {
            arm.transform.rotation = Quaternion.Euler(0, 0, attackangle);
        }
        else
        {
            arm.transform.rotation = Quaternion.Euler(0, 0, 180 + attackangle);
        }

        if (Input.GetMouseButtonDown(0) && !attackCD)
        {
            lasersfx.Play();
            attack.SetActive(true);
        }

        if (Input.GetMouseButton(0) && !attackCD)
        {
            attacktimer += Time.deltaTime;
            if(attacktimer>=5)
            {
                attackCD = true;
            }
            if (!reversed)
            {
                attack.transform.rotation = Quaternion.Euler(0, 0, attackangle);
            }
            else
            {
                attack.transform.rotation = Quaternion.Euler(0, 0, 180 + attackangle);
            }
        }
        else
        {
            if(attacktimer > 0 && !attackCD)
            {
                attacktimer -= Time.deltaTime * 2;
                if(attacktimer<0)
                {
                    attacktimer = 0;
                }
            }
            else if(attacktimer > 0 && attackCD)
            {
                attacktimer -= Time.deltaTime;
                if (attacktimer < 0)
                {
                    attacktimer = 0;
                    attackCD = false;
                }
            }
     
            lasersfx.Stop();
            attack.SetActive(false);
        }
        Color temp = arm.GetComponent<SpriteRenderer>().color;
        temp.b = 1 - attacktimer * 0.2f;
        temp.g = 1 - attacktimer * 0.2f;
        arm.GetComponent<SpriteRenderer>().color = temp;
    }

    void horizontal()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            LayerMask mask = (1 << 9) + (1 << 8);
            Collider2D wall = Physics2D.OverlapBox(rightcheck.position, new Vector2(0.05f, 0.55f), 0.0f, mask);
            if (wall != null && (wall.CompareTag("ground") || wall.CompareTag("moveground")))
            {
            }
            else
            {
                transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
                movetime += Time.deltaTime;
                reversed = false;
                if (movetime < .1f)
                {
                    GetComponent<SpriteRenderer>().sprite = moving1;
                }
                if (movetime > .1f)
                {
                    GetComponent<SpriteRenderer>().sprite = moving2;
                }          
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            LayerMask mask = (1 << 9) + (1 << 8);
            Collider2D wall = Physics2D.OverlapBox(rightcheck.position, new Vector2(0.05f, 0.55f), 0.0f, mask);
            if (wall != null && (wall.CompareTag("ground") || wall.CompareTag("moveground")))
            {

            }
            else
            {
                transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
                movetime += Time.deltaTime;
                reversed = true;
                if (movetime < 0.1f)
                {
                    GetComponent<SpriteRenderer>().sprite = moving1;
                }
                if (movetime > 0.1f)
                {
                    GetComponent<SpriteRenderer>().sprite = moving2;
                }
            }
        }
        else
        {
            movetime = 0;
            GetComponent<SpriteRenderer>().sprite = idle;
        }
    }

    void vertical()
    {
        if(jumptimer > 0)
        {
            jumptimer -= Time.deltaTime;
        }

        if (jumptimer <= 0)
        {
            Collider2D ground = Physics2D.OverlapCircle(groundcheck.position, 0.06f, 1<<9);
            if (ground != null && ground.CompareTag("ground"))
            {
                grounded = true;
                jumping = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else if (ground != null && ground.CompareTag("moveground"))
            {
                onmoveground = true;
                grounded = true;
                jumping = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                if (hitmoveground)
                {
                    GetComponent<Rigidbody2D>().velocity = ground.GetComponent<Rigidbody2D>().velocity;
                    hitmoveground = false;
                }
            }
            else
            {
                hitmoveground = false;
                onmoveground = false;
                grounded = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            grounded = false;
            hitmoveground = false;
            onmoveground = false;
            jumpsfx.Play();
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4.75f);
            jumptimer = 0.1f;
        }
        if ((Input.GetButton("Jump")))
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }

        if (jumping)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                GetComponent<Rigidbody2D>().gravityScale = 3;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 3;
        }
    }
}
