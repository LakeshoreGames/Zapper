using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endeath : MonoBehaviour {
    public AudioSource deathsound;

	// Use this for initialization
	void Start () {
        deathsound.Play();
        Destroy(gameObject, 0.5f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
