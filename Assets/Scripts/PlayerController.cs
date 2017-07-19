using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject game;
    public GameObject enemyGenerator;

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;

        if (gamePlaying && Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateState("PlayerFlyUp");
        }else if (gamePlaying && Input.GetKeyDown(KeyCode.DownArrow))
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("PlayerCrash");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
        }

    }

}
