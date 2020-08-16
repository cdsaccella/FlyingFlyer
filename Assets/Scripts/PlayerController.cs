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

        GameState gameState = game.GetComponent<GameController>().gameState;
        bool gamePlaying = gameState == GameState.Playing;
        bool gameFinished = gameState == GameState.Ended;

        if (gamePlaying && Input.GetKey(KeyCode.UpArrow))
        {
            UpdateState("PlayerFlyUp");
        }
        else if (gamePlaying && Input.GetKey(KeyCode.DownArrow))
        {     
            UpdateState("PlayerFlyDown");
        }else if (gamePlaying)
        {
            UpdateState("PlayerIdle");
        }
        else if (gameFinished)
        {
            UpdateState("PlayerDestroyed");
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
                case "PlayerDestroyed":
                    Vector2 emptyVelocity = new Vector2(0, 0);
                    gameObject.GetComponent<Rigidbody2D>().velocity = emptyVelocity;
                    animator.SetBool("movingUp", false);
                    animator.SetBool("movingDown", false);
                    animator.SetBool("playerDestroyed", true);
                    break;
                case "PlayerImpulseUp":
                    Vector2 downVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

                    if (downVelocity.y < 0)
                    {
                        var downSpeed = downVelocity.magnitude;
                        var downDirection = Vector3.Reflect(downVelocity.normalized, new Vector2(0, 1));
                        gameObject.GetComponent<Rigidbody2D>().velocity = downDirection * Mathf.Max(downSpeed, 1f);
                    }
 
                    animator.SetBool("movingDown", false);
                    animator.SetBool("movingUp", true);
                    break;
                case "PlayerImpulseDown":
                    Vector2 upVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

                    if (upVelocity.y > 0)
                    {
                        var upSpeed = upVelocity.magnitude;
                        var upDirection = Vector3.Reflect(upVelocity.normalized, new Vector2(0, -1));
                        gameObject.GetComponent<Rigidbody2D>().velocity = upDirection * Mathf.Max(upSpeed, 1f);
                    }        

                    animator.SetBool("movingUp", false);
                    animator.SetBool("movingDown", true);
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
