﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour {

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        
    }
}
