using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Idle, Playing, Ended }

public class GameController : MonoBehaviour {

    [Range(0, 0.2f)]
    public float unitSpeed = 0.01f;
    public RawImage background;
    public RawImage background2;
    public RawImage background3;
    public RawImage background4;
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
        float finalUnitSpeed = unitSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalUnitSpeed, 0f, 1f, 1f);
        background2.uvRect = new Rect(background2.uvRect.x + (finalUnitSpeed * 2), 0f, 1f, 1f);
        background3.uvRect = new Rect(background3.uvRect.x + (finalUnitSpeed * 3), 0f, 1f, 1f);
        background4.uvRect = new Rect(background4.uvRect.x + (finalUnitSpeed * 4), 0f, 1f, 1f);
    }
}
