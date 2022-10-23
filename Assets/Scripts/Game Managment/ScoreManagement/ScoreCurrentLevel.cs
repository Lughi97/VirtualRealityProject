using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Calculation of the Score of a single Level
/// </summary>
public class ScoreCurrentLevel : MonoBehaviour
{
    [Header("COINS UI")]
    public TextMeshProUGUI bronzeText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI goldText;
    public int bronzeCounter = 0;
    public int silverCounter = 0;
    public int goldCounter = 0;
    [SerializeField] public CanvasGroup coinCanvas;
    private int bronzeScore;
    private int silverScore;
    private int goldScore;
    public Transform[] coins;

    [Header("Score Level UI ")]
    public GameObject LevelComplete;
    public TextMeshProUGUI FinalScoreLevel;
    public TextMeshProUGUI HighScoreLevel;
    public TextMeshProUGUI needToComplete;
    public GameObject BoardMenu;


    public Material trasparentMaterial;
    public Material[] currentMaterial;

    public static ScoreCurrentLevel instance;





    public bool CR_running = false;

    public bool fadeOut = false;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        LevelComplete.gameObject.SetActive(false);
        needToComplete.gameObject.SetActive(false);
        BoardMenu.SetActive(false);
        coinCanvas.alpha = 0;
        bronzeText.text = bronzeCounter.ToString();
        silverText.text = silverCounter.ToString();
        goldText.text = goldCounter.ToString();
        FinalScoreLevel.text = FinalScoreLevel.ToString();
        HighScoreLevel.text = HighScoreLevel.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentMaterial.Length == 0)
        {
            currentMaterial = new Material[coins.Length];

            for (int i = 0; i < coins.Length; i++)
            {
                currentMaterial[i] = coins[i].GetComponent<MeshRenderer>().material;

                coins[i].GetComponent<MeshRenderer>().material = trasparentMaterial;

            }
        }
        if (GameManager.Instance.restartLevel == true)
        {
            for (int i = 0; i < coins.Length; i++)
                coins[i].GetComponent<MeshRenderer>().material = trasparentMaterial;
          
            BoardMenu.SetActive(false);
            bronzeCounter = 0;
            silverCounter = 0;
            goldCounter = 0;
            bronzeText.text = bronzeCounter.ToString();
            silverText.text = silverCounter.ToString();
            goldText.text = goldCounter.ToString();
            coinCanvas.alpha = 0f;
            StopCoroutine(fadeCoin());
            StopCoroutine(notLevelComplete());
        }


    }
    //We collect the coin and get the information about the score
    public void AddCoin(CollectCollectabales coin)
    {
       
       
        coinCanvas.alpha = 1;
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].GetComponent<MeshRenderer>().material = currentMaterial[i];
        }
        fadeOut = true;
        if (CR_running)
        {
            StopCoroutine(fadeCoin());
            CR_running = false;
        }
        else StartCoroutine(fadeCoin());
        switch (coin.typeScore)
        {
            case TypeScore.bronze:
                bronzeCounter++;
                bronzeText.text = bronzeCounter.ToString();
                if (bronzeScore == 0) bronzeScore = coin.contentScore;
                ScoringSystem.Instance.getCollectedCoins(bronzeCounter,coin);
                break;
            case TypeScore.silver:
                silverCounter++;
                silverText.text = silverCounter.ToString();
                if (silverScore == 0) silverScore = coin.contentScore;
                ScoringSystem.Instance.getCollectedCoins(silverCounter, coin);
                break;
            case TypeScore.gold:
                goldCounter++;
                goldText.text = goldCounter.ToString();
                if (goldScore == 0) goldScore = coin.contentScore;
                ScoringSystem.Instance.getCollectedCoins(goldCounter, coin);
                break;


        }
    }
    // Fade out the coins
    private IEnumerator fadeCoin()
    {

        CR_running = true;
        while (fadeOut)
        {
            //  Debug.Log(coinCanvas.alpha);
            if (coinCanvas.alpha >= 0)
            {
                coinCanvas.alpha -= 0.05f;
           
            }
            if (coinCanvas.alpha == 0)
            {
                fadeOut = false;
                for (int i = 0; i < currentMaterial.Length; i++)
                    coins[i].GetComponent<MeshRenderer>().material = trasparentMaterial;
            }// && coin.GetComponent<MeshRenderer>().material.color.a == 0) 
            yield return new WaitForSeconds(0.5f);


        }
        CR_running = false;

    }

    //Calculate the total level score gather by the collection of coins
    public void CalculateCoins()
    {

        int finalLevelScore = bronzeCounter * bronzeScore + silverCounter * silverScore + goldCounter * goldScore;
        ScoringSystem.Instance.getCurrentLevlCoinScore(finalLevelScore);

    }
    // We activate the board with the highscore and current score 
    public void ShowCurrentLevelScore()
    {
        BoardMenu.gameObject.SetActive(true);
        LevelComplete.gameObject.SetActive(true);
        coinCanvas.gameObject.SetActive(false);
        FinalScoreLevel.text = "Final Level Score: " + ScoringSystem.Instance.currentScore;

        switch (GameManager.Instance.typeScene)
        {
            case SceneLevel.Level1:
                HighScoreLevel.text = "HighScore: " + ScoringSystem.Instance.HighScoreLevel1[0];
                break;
            case SceneLevel.Level2:
                HighScoreLevel.text = "HighScore: " + ScoringSystem.Instance.HighScoreLevel2[0];
                break;
            case SceneLevel.Level3:
                HighScoreLevel.text = "HighScore: " + ScoringSystem.Instance.HighScoreLevel3[0];
                break;

        }
    
    }
    // show how many coins we need to collect to complete the level
    public IEnumerator notLevelComplete()
    {
        BoardMenu.gameObject.SetActive(true);
        needToComplete.gameObject.SetActive(true);
        needToComplete.text = "You need to collect " + GameManager.Instance.tmpLevelManager.GetComponent<LevelManagement>().coinsLeft + " Coins to complete the level";
        if (GameManager.Instance.isGameOver)
        {
            BoardMenu.gameObject.SetActive(false);
            needToComplete.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(3f);
        BoardMenu.gameObject.SetActive(false);
        needToComplete.gameObject.SetActive(false);
    }
}
