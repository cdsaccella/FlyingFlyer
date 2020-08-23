using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;

    private int score = 0;

    void Start()
    {
        scoreManager = this;
    }

    public int IncreaseScore(int score = 1)
    {
        this.score += score;
        return this.score;
    }

    public int GetScore()
    {
        return this.score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("score", 0);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("score", score);
    }
}
