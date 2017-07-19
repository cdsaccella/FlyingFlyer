using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateState("PlayerFlyUp");
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpdateState("PlayerFlyDown");
        }
	}

    void UpdateState(string gameState = null)
    {
        if (gameState != null) 
        {
            animator.Play(gameState);
        }
    }
    
}
