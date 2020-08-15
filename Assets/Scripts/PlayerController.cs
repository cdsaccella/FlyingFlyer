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

        if (gamePlaying && Input.GetKey(KeyCode.UpArrow))
        {
            UpdateState("PlayerFlyUp");
        }
        else if (gamePlaying && Input.GetKey(KeyCode.DownArrow))
        {     
            UpdateState("PlayerFlyDown");
        }else
        {
            UpdateState("PlayerIdle");
        }
	}

    void UpdateState(string gameState = null)
    {
        float forceValue = 1000f * Time.deltaTime;

        if (gameState != null) 
        {
            switch (gameState)
            {
                case "PlayerFlyUp":
                    Vector2 upForce = new Vector2(0, forceValue);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(upForce);
                    animator.SetBool("movingDown", false);
                    animator.SetBool("movingUp", true);
                    break;
                case "PlayerFlyDown":
                    Vector2 downForce = new Vector2(0, -forceValue);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(downForce);
                    animator.SetBool("movingUp", false);
                    animator.SetBool("movingDown", true);
                    break;
                case "PlayerIdle":
                    animator.SetBool("movingUp", false);
                    animator.SetBool("movingDown", false);
                    break;
                default:
                    break;
            }
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
