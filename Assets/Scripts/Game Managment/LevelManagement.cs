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
   
    // Update is called once per frame (search al the coins in the scene
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


    }
    // calculate how many coins are in the current level
    private void calculateTotalCoins()
    {
        totalCoinsInWorld = coins.Length + (creates.Length) * 6;
        totalCoinsToCompleteLevel();
    }
    // how many coins needed to complete the level
    private void totalCoinsToCompleteLevel()
    {
        // Debug.Log("hello");
        //  Debug.Log((totalCoinsInWorld);
        toComplete = (int)((totalCoinsInWorld / 100) * 50);
        coinsLeft = toComplete;
    }
    // check how many coins are left to complete the level
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
