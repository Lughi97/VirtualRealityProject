using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Level management: check if the coin that the player
/// colleceted is enough to end the level
/// </summary>
public class LevelManagement : MonoBehaviour
{

    public float totalCoinsInWorld;
    //public float collected;
    public float coinsLeft;
    public float toComplete;
    [SerializeField]
    private GameObject[] coins;
    [SerializeField]
    private GameObject[] creates;

    private bool calculatedCoinsAmount = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coins.Length == 0)
        {
            coins = GameObject.FindGameObjectsWithTag("Coins");
        }
        if (creates.Length == 0)
        {
            creates = GameObject.FindGameObjectsWithTag("Create");
        }
        if (!calculatedCoinsAmount)
        {
            calculatedCoinsAmount = true;
            calculateTotalCoins();
        }
        //if (coinsLeft != 0)
        //    coinsLeftToComplete(GameManager.Instance.tempPlayer.GetComponent<PlayerWithPhysic>().coinsCollected.Count);


    }

    private void calculateTotalCoins()
    {
        totalCoinsInWorld = coins.Length + (creates.Length) * 6;
        totalCoinsToCompleteLevel();
    }
    private void totalCoinsToCompleteLevel()
    {
        // Debug.Log("hello");
        //  Debug.Log((totalCoinsInWorld);
        toComplete = (int)((totalCoinsInWorld / 100) * 50);
        coinsLeft = toComplete;
    }

    public float coinsLeftToComplete(int collectedByPlayer)
    {
        Debug.Log("CollectedbyPlayer: " + collectedByPlayer);
        //Debug.Log("To COmplete: "+ toComplete);
        coinsLeft = toComplete-collectedByPlayer;
        if (coinsLeft < 0) coinsLeft = 0;
        Debug.Log("COINS LEFT: " + coinsLeft);
        return coinsLeft;
    }
}
