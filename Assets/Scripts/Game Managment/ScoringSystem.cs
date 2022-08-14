using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class ScoringSystem : MonoBehaviour
{
    //public ScoringSystem instance;
    public static ScoringSystem instance { get; private set; }
    public int Score = 0;
    public int scoreCoin = 0;
    public int scoreDistance = 0;
    public int HighScore = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getCurrentLevlCoinScore(int score)
    {
        scoreCoin = score;
        Debug.Log("CoinScore: " + scoreCoin);
    }
    public void getCurrentLevelDistanceScore(int score)
    {
        scoreDistance = score;
        Debug.Log("Score distance: " + scoreDistance);
    }
    public void getCurrentLevelTotalScore()
    {
        Debug.Log("HELLO");
        Score = scoreCoin + scoreDistance;
        Debug.Log("Final Level Score: " + Score);
        if (HighScore < Score) HighScore = Score;
        Debug.Log("Level HighScore: " + HighScore);

    }
    public void resetCurrentLevelScore()
    {
        Score = 0;
        scoreCoin = 0;
        scoreDistance = 0;
        Debug.Log(Score);
    }
}
