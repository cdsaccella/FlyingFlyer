using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Idle, Playing, Ended }

public class GameController : MonoBehaviour {

    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public GameObject gameIdle;

    
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gameState == GameState.Idle && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))){
            gameState = GameState.Playing;
            gameIdle.SetActive(false);
            //  player.SendMessage("UpdateState", "PlayerFly");
            enemyGenerator.SendMessage("StartGenerator");
        }else if(gameState == GameState.Playing)
        {
            Parallax();
        }else if(gameState == GameState.Ended)
        {
            // TO DO
        }

	}

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
    }
}
