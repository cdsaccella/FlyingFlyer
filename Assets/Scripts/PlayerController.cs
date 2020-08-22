﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IFinalizable {

    public GameObject game;
    public GameObject enemyGenerator;
    public AudioClip dieClip;
    public float force = 1000f;

    public ParticleSystem fire;

    private Animator animator;
    private AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
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
        float forceValue = force * Time.deltaTime;

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
        if (other.gameObject.tag == "Enemy") { 
            game.GetComponent<GameController>().SendMessage("EndGame");    
        }
    }

    void GameReady()
    {
        game.GetComponent<GameController>().gameState = GameState.Ready;
    }

    void FirePlay()
    {
        fire.Play();
    }

    void FireStop()
    {
        fire.Stop();
    }

    public void EndGame()
    {
        UpdateState("PlayerCrash");
        game.GetComponent<AudioSource>().Stop();
        audioPlayer.clip = dieClip;
        audioPlayer.Play();
        FireStop();
    }
}
