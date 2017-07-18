using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public GameObject gameIdle;

    public enum GameState { Idle, Playing }
    public GameState gameState = GameState.Idle;

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gameState == GameState.Idle && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))){
            gameState = GameState.Playing;
            gameIdle.SetActive(false);
            player.SendMessage("UpdateState", "PlayerFly");
        }else if(gameState == GameState.Playing)
        {
            Parallax();
        }

	}

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
    }
}
