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
    private void Awake()
    {
        instance = this;    
        
    }
    // Start is called before the first frame update
    void Start()
    {
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
            if (coinCanvas.alpha >= 0)
                coinCanvas.alpha -= 0.05f;
            if (coinCanvas.alpha == 0) fadeOut = false;
            yield return new WaitForSeconds(0.05f);
            
           
        }
        CR_running = false;

    }
}
