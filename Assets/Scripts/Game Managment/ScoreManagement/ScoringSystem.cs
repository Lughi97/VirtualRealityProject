using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class coinValue
{
    public TypeScore type;
    public int counter;
}
[Serializable]
public class ScoringSystem : Singleton<ScoringSystem>
{
    //public ScoringSystem instance;
    // public static ScoringSystem instance { get; private set; }
    [Header("Highscores")]
    public List<int> HighScoreLevel1 = new List<int>();
    public List<int> HighScoreLevel2 = new List<int>();
    public List<int> HighScoreLevel3 = new List<int>();

    [SerializeField]
    private int scoreCoin;
    //[SerializeField
    // public CoinValue currentCoinValue;
    [SerializeField]
    coinValue currentCoin;
 //   [SerializeField]
    public List<coinValue> listCoins = new List<coinValue>();
   // public Dictionary<TypeScore, int> coinCollection = new Dictionary<TypeScore, int>();
    [SerializeField]
    private int scoreDistance;
    public int currentScore = 0;
    public int HighScore = 0;
    public int maxScoreelements = 8;
    // Start is called before the first frame update
    private void Awake()
    {
        if (FindObjectsOfType(typeof(ScoringSystem)).Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
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
        currentScore = scoreCoin + scoreDistance;
        updateLevelHighScore();
    }
    public void getCollectedCoins(int counter, CollectCollectabales coin)
    {
        currentCoin.type = coin.typeScore;
        currentCoin.counter=counter;
        if (listCoins.Count == 0) listCoins.Add(currentCoin);
        else
            foreach (coinValue coinType in listCoins)
            {
                //Debug.Log(coinType);

                switch (coinType.type)
                {
                    case TypeScore.bronze:
                        if (coinType.type == currentCoin.type)
                            coinType.counter ++;
                        break;
                    case TypeScore.silver:
                        if (coinType.type == currentCoin.type)
                            coinType.counter ++;
                        break;
                    case TypeScore.gold:
                        if (coinType.type == currentCoin.type)
                            coinType.counter ++;
                        break;

                }
            }
        
    

    }
    public void resetCurrentLevelScore()
    {
        currentScore = 0;
        scoreCoin = 0;
        scoreDistance = 0;
        // Debug.Log(Score);
    }

    public void updateLevelHighScore()
    {
        //Debug.Log("HELLO THERE");
        switch (GameManager.Instance.typeScene)
        {
            case SceneLevel.Level1:
                foreach (int element in HighScoreLevel1)
                {
                    if (currentScore > element)
                    {
                        HighScoreLevel1.Add(currentScore);
                        break;
                    }

                }
                HighScoreLevel1.Sort();
                HighScoreLevel1.Reverse();

                if (HighScoreLevel1.Count > maxScoreelements)
                    HighScoreLevel1.RemoveAt(HighScoreLevel1.Count - 1);
                break;
            case SceneLevel.Level2:
                foreach (int element in HighScoreLevel2)
                {
                    if (currentScore > element)
                    {
                        HighScoreLevel2.Add(currentScore);
                        break;
                    }
                }
                HighScoreLevel2.Sort();
                HighScoreLevel2.Reverse();
                if (HighScoreLevel2.Count > maxScoreelements)
                    HighScoreLevel2.RemoveAt(HighScoreLevel2.Count - 1);
                break;
            case SceneLevel.Level3:
                foreach (int element in HighScoreLevel3)
                {
                    if (currentScore > element)
                    {
                        HighScoreLevel3.Add(currentScore);
                        break;
                    }
                }
                HighScoreLevel3.Sort();
                HighScoreLevel3.Reverse();
                if (HighScoreLevel3.Count > maxScoreelements)
                    HighScoreLevel3.RemoveAt(HighScoreLevel3.Count - 1);
                break;
        }
    }
}
