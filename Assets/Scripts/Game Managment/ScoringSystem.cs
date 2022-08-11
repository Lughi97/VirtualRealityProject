using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ScoringSystem : MonoBehaviour
{
    //public ScoringSystem instance;
    public static ScoringSystem instance { get; private set; }
    public int Score;
    public int HighScore;
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


    public void getCurrentLevelTotalScore(int score)
    {
        Score = score;
        if (HighScore < Score) HighScore = Score;
        Debug.Log(HighScore);

    }
    public void resetCurrentLevelScore()
    {
        Score = 0;
    }
}
