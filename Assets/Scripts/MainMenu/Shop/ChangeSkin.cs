using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeSkin : MonoBehaviour
{
    public PlayerType skinToEquip;
    public TextMeshProUGUI CoinNeed;
    public int coinBronze;
    public int coinSilver;
    public int coinGold;

    [SerializeField] private int totalCoinsBronze;
    [SerializeField] private int totalCoinsSilver;
    [SerializeField] private int totalCoinsGold;
    public TextMeshProUGUI needToComplete;

    public void Start()
    {
        //skinToEquip = gameObject.GetComponent<Renderer>().material;
       
        if (!skinToEquip.paid)
        {
            coinBronze = skinToEquip.costBronze;
            coinSilver = skinToEquip.costSilver;
            coinGold = skinToEquip.costGold;
            needToComplete.text = coinBronze.ToString() + " bronze coins," + coinSilver.ToString() + " silver coins," + coinGold.ToString() + " gold coins";
        }
        else
        {
            needToComplete.text = " ";
        }
        // needToComplete.text =coinBronze.ToString() + " bronze coins," + coinSilver.ToString() + " silver coins," + coinGold.ToString() + " gold coins";

        foreach (coinValue coins in ScoringSystem.Instance.listCoins)
        {
            switch (coins.type)
            {
                case TypeScore.bronze:
                    totalCoinsBronze = coins.counter;

                    break;
                case TypeScore.silver:
                    totalCoinsSilver = coins.counter;
                    break;
                case TypeScore.gold:
                    totalCoinsGold = coins.counter;
                    break;

            }
        }
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(other.gameObject.GetComponentInParent<SkinLoader>().tmpSkinLoad.name );
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(totalCoinsBronze >= coinBronze && totalCoinsSilver >= coinSilver && totalCoinsGold >= coinGold);
            if (totalCoinsBronze >= coinBronze && totalCoinsSilver >= coinSilver && totalCoinsGold >= coinGold && !skinToEquip.paid)
            {
                foreach (coinValue coins in ScoringSystem.Instance.listCoins)
                {
                    switch (coins.type)
                    {
                        case TypeScore.bronze:
                            coins.counter -= coinBronze;
                            totalCoinsBronze = coins.counter;
                            coinBronze = 0;

                            break;
                        case TypeScore.silver:
                            coins.counter -= coinSilver;
                            totalCoinsSilver = coins.counter;
                            coinSilver = 0;
                            break;
                        case TypeScore.gold:
                            coins.counter -= coinGold;
                            totalCoinsGold = coins.counter;
                            coinGold = 0;
                            break;

                    }
                }
                skinToEquip.paid = true;
                GameManager.Instance.updateSkin(skinToEquip);
              //  SaveSystem.SaveSkin(skinToEquip);
                needToComplete.text = "";
            }
            else if(skinToEquip.paid)
            {
                GameManager.Instance.updateSkin(skinToEquip);
                SaveSystem.SaveSkin(skinToEquip);
            }
        }
        else if (skinToEquip.paid)
            GameManager.Instance.updateSkin(skinToEquip);
        else
        {
            Debug.Log("Not enough Money");
        }

    }
}
