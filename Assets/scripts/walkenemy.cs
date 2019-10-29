using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkenemy : MonoBehaviour {
    public Transform rightcheck;
    public bool goingleft;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (goingleft)
        {
            transform.position += new Vector3(-0.75f * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            LayerMask mask = (1 << 9) + (1 << 8);
            Collider2D wall = Physics2D.OverlapBox(rightcheck.position, new Vector2(0.05f, 0.55f), 0.0f, mask);
            if (wall != null && (wall.CompareTag("ground") || wall.CompareTag("moveground")))
            {
                goingleft = false;
            }
        }
        else
        {
            transform.position += new Vector3(0.75f * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            LayerMask mask = (1 << 9) + (1 << 8);
            Collider2D wall = Physics2D.OverlapBox(rightcheck.position, new Vector2(0.05f, 0.55f), 0.0f, mask);
            if (wall != null && (wall.CompareTag("ground") || wall.CompareTag("moveground")))
            {
                goingleft = true;
            }
        }
    }
}
