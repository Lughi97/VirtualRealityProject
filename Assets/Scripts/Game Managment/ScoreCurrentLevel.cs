using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI bronzeText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI goldText;


    public GameObject LevelComplete;
    public TextMeshProUGUI FinalScoreLevel;
    public TextMeshProUGUI HighScoreLevel;

    public Material trasparentMaterial;
    public Material[] currentMaterial;

    public static ScoreCurrentLevel instance;


    public int bronzeCounter = 0;
    public int silverCounter = 0;
    public int goldCounter = 0;

    private int bronzeScore;
    private int silverScore;
    private int goldScore;
    public bool CR_running = false;
    [SerializeField] public CanvasGroup coinCanvas;
    // private bool fadeIn = false;
    public bool fadeOut = false;
    public Transform[] coins;
    //[SerializeField]
    //private Color[] coinColor;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        LevelComplete.gameObject.SetActive(false);
        // coin = coinCanvas.gameObject.transform.GetChild(0).transform.GetChild(0);
        //Color currentCoin = coin.GetComponent<MeshRenderer>().material.color;

        //  coin.GetComponent<MeshRenderer>().material.color = currentCoin;
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
            Debug.Log("EEEEEEEEY");
            currentMaterial = new Material[coins.Length];
          
            for (int i = 0; i < coins.Length; i++)
            {

                // coinColor[i] = coin[i].GetComponent<MeshRenderer>().material.color;
                //  coinColor[i].a = 0f;
                currentMaterial[i] = coins[i].GetComponent<MeshRenderer>().material;
                Debug.Log(currentMaterial[i]);
                coins[i].GetComponent<MeshRenderer>().material = trasparentMaterial;
                Debug.Log(coins[i].GetComponent<MeshRenderer>().material);

                //coin[i].GetComponent<MeshRenderer>().material.color = coinColor[i];
            }
        }
        if (GameManager.instance.restartLevel == true)
        {
            bronzeCounter = 0;
            silverCounter = 0;
            goldCounter = 0;
            bronzeText.text = bronzeCounter.ToString();
            silverText.text = silverCounter.ToString();
            goldText.text = goldCounter.ToString();
            coinCanvas.alpha = 0f;
            StopAllCoroutines();
        }


    }
    public void AddCoin(CollectCollectabales coin)
    {
        //Debug.Log(coin.typeScore);
        //CalculateCoins();
        coinCanvas.alpha = 1;
        for (int i = 0; i < coins.Length; i++)
        {

            // coinColor[i] = coin[i].GetComponent<MeshRenderer>().material.color;
            //  coinColor[i].a = 0f;
            coins[i].GetComponent<MeshRenderer>().material = currentMaterial[i];

            //coin[i].GetComponent<MeshRenderer>().material.color = coinColor[i];
        }

        //  Color currentCoin = coin.GetComponent<MeshRenderer>().material.color;
        //currentCoin.a = 1;
        // coin.GetComponent<MeshRenderer>().material.color = currentCoin;
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
                break;
            case TypeScore.silver:
                silverCounter++;
                silverText.text = silverCounter.ToString();
                if (silverScore == 0) silverScore = coin.contentScore;
                break;
            case TypeScore.gold:
                goldCounter++;
                goldText.text = goldCounter.ToString();
                if (goldScore == 0) goldScore = coin.contentScore;
                break;


        }
    }

    private IEnumerator fadeCoin()
    {

        CR_running = true;
        while (fadeOut)
        {
            Debug.Log(coinCanvas.alpha);
            if (coinCanvas.alpha >= 0)
            {
                coinCanvas.alpha -= 0.05f;
                //   for (int i = 0; i < coin.Length; i++)
                //  {
                //coinColor[i] = coin[i].GetComponent<MeshRenderer>().material.color;
                // coinColor[i].a -=0.05f;
                //  }
                //  Color currentCOlor = coin.GetComponent<MeshRenderer>().material.color;
                // currentCOlor.a -= 0.05f;
                // coin.GetComponent<MeshRenderer>().material.color = currentCOlor;
            }
            if (coinCanvas.alpha == 0)
            {
                fadeOut = false;
                for (int i = 0; i < currentMaterial.Length; i++)
                    coins[i].GetComponent<MeshRenderer>().material =  trasparentMaterial;
            }// && coin.GetComponent<MeshRenderer>().material.color.a == 0) 
            yield return new WaitForSeconds(0.5f);


        }
        CR_running = false;

    }

    public void CalculateCoins()
    {
        int finalLevelScore = bronzeCounter * bronzeScore + silverCounter * silverScore + goldCounter * goldScore;
        //Debug.Log(finalLevelScore);
        ScoringSystem.instance.getCurrentLevlCoinScore(finalLevelScore);

    }

    public void ShowCurrentLevelScore()
    {
        LevelComplete.gameObject.SetActive(true);
        coinCanvas.gameObject.SetActive(false);
        FinalScoreLevel.text = "Final Level Score: " + ScoringSystem.instance.Score;
        HighScoreLevel.text = "HighScore: " + ScoringSystem.instance.HighScore;
    }
}
