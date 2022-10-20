using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Show all the coins collected by the player
/// </summary>
public class ShowCoins : MonoBehaviour
{
    public TextMeshProUGUI bronzeText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI goldText;
    public GameObject coinCanvas;
    public int bronzeCounter = 0;
    public int silverCounter = 0;
    public int goldCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        coinCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Instance.typeScene);
        if (GameManager.Instance.typeScene == SceneLevel.Shop)
        {
            coinCanvas.SetActive(true);
            StartCoroutine(loadCoinCounter());
        }
        else
            coinCanvas.SetActive(false);
    }
    private IEnumerator loadCoinCounter()
    {
        yield return new WaitForSeconds(1f);
        foreach (coinValue coin in ScoringSystem.Instance.listCoins)
        {
            switch (coin.type)
            {
                case TypeScore.bronze:
                    bronzeCounter = coin.counter;
                    bronzeText.text = bronzeCounter.ToString();
                    break;
                case TypeScore.silver:
                    silverCounter = coin.counter;
                    silverText.text = silverCounter.ToString();
                    break;
                case TypeScore.gold:
                    goldCounter = coin.counter;
                    goldText.text = goldCounter.ToString();
                    break;

            }
        }
    }
}
