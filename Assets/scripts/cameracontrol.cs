using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour {

    public Player player;
    public bool movingright;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            speed = player.speed;
            if(player.transform.localScale.x < 0)
            {
                movingright = false;
            }
            else
            {
                movingright = true;
            }

            if (movingright)
            {
                if (player.transform.position.x > GetComponent<Transform>().position.x - 0.35f)
                {
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                }           
            }
            else
            {
                if (player.transform.position.x < GetComponent<Transform>().position.x + 0.65f)
                {
                    transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                }
            }

            if (player.transform.position.y > GetComponent<Transform>().position.y + 0.6f)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            if (player.transform.position.y < transform.position.y - 0.4f)
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }

            if (player.onmoveground && !player.jumping)
            {
                GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }
}
