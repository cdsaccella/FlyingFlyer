using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, Ended, Ready }

public class GameController : MonoBehaviour, IFinalizable
{

    [Range(0, 0.2f)]
    public float unitSpeed = 0.01f;
    public float timeScore = 0.5f;

    public RawImage background;
    public RawImage background2;
    public RawImage background3;
    public RawImage background4;
    public GameObject gameIdle;
    public GameObject gameScore;
    public Text gamePoints;

    public GameState gameState = GameState.Idle;   

    public GameObject player;
    public GameObject enemyGenerator;

    public GameObject[] finalizables;

    private AudioSource musicPlayer;

    private int points = 0;

    // Use this for initialization
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        finalizables = new GameObject[] { player, enemyGenerator };
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow));

        if (gameState == GameState.Idle && userAction)
        {
            gameState = GameState.Playing;
            gameIdle.SetActive(false);
            gameScore.SetActive(true);
            player.SendMessage("FirePlay");
            enemyGenerator.SendMessage("StartGenerator");
            InvokeRepeating("IncreasePoints", timeScore, timeScore);
            musicPlayer.Play();
        }
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }
        else if (gameState == GameState.Ready)
        {
            if (userAction)
            {
                RestartGame();
            }
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

    public void RestartGame()
    {
        SceneManager.LoadScene("Principal");
    }

    public void EndGame()
    {
        gameState = GameState.Ended;
        CancelInvoke("IncreasePoints");
        points = 0;
        foreach (var finalizable in finalizables)
        {
            finalizable.SendMessage("EndGame");
        }
    }

    public void IncreasePoints()
    {
        points++;
        gamePoints.text = points.ToString();
    }
}
