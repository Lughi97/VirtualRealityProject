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

    public static ScoreCurrentLevel instance;


    public int bronzeCounter = 0;
    public int silverCounter = 0;
    public int goldCounter = 0;

    private bool CR_running = false;
    [SerializeField] private CanvasGroup coinCanvas;
   // private bool fadeIn = false;
    public bool fadeOut = false;
    public Transform coinBronze;
    private void Awake()
    {
        instance = this;    
        
    }
    // Start is called before the first frame update
    void Start()
    {
         coinBronze = coinCanvas.gameObject.transform.GetChild(0).transform.GetChild(0);
       Color currentCoin = coinBronze.GetComponent<MeshRenderer>().material.color;
        currentCoin.a = 0;
        coinBronze.GetComponent<MeshRenderer>().material.color = currentCoin;
        coinCanvas.alpha = 0;  
        bronzeText.text = bronzeCounter.ToString();
            silverText.text = silverCounter.ToString();
            goldText.text = goldCounter.ToString();
        

    }
   
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.restartLevel == true)
        {
            bronzeCounter = 0;
            silverCounter = 0;
            goldCounter = 0;
            bronzeText.text = bronzeCounter.ToString();
            silverText.text = silverCounter.ToString();
            goldText.text = goldCounter.ToString();
        }


    }
    public void AddCoin(CollectCollectabales coin)
    {
        Debug.Log(coin.typeScore);
        coinCanvas.alpha = 1;
        Color currentCoin = coinBronze.GetComponent<MeshRenderer>().material.color;
        currentCoin.a = 1;
        coinBronze.GetComponent<MeshRenderer>().material.color = currentCoin;
        fadeOut = true;
        if (CR_running)
        {
            StopCoroutine(fadeCoin());
            StartCoroutine(fadeCoin());
        

        }
        else StartCoroutine(fadeCoin());
        switch (coin.typeScore)
        {
            case TypeScore.bronze:
                bronzeCounter++;
                bronzeText.text = bronzeCounter.ToString();
                break;
            case TypeScore.silver:
                silverCounter++;
                silverText.text = silverCounter.ToString();
                break;
            case TypeScore.gold:
                goldCounter++;
                goldText.text = goldCounter.ToString();
                break;


        }
    }

    private IEnumerator fadeCoin()
    {
        
        CR_running = true;
        while (fadeOut)
        {
            if (coinCanvas.alpha >= 0 && coinBronze.GetComponent<MeshRenderer>().material.color.a >= 0)
            {
                coinCanvas.alpha -= 0.05f;
                Color currentCOlor = coinBronze.GetComponent<MeshRenderer>().material.color;
                currentCOlor.a -= 0.05f;
                coinBronze.GetComponent<MeshRenderer>().material.color = currentCOlor;
            }
            if (coinCanvas.alpha == 0 && coinBronze.GetComponent<MeshRenderer>().material.color.a == 0) fadeOut = false;
            yield return new WaitForSeconds(0.5f);
            
           
        }
        CR_running = false;

    }
}
