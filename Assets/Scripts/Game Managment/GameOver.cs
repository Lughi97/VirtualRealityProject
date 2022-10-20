using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
/// <summary>
/// Behavior when the player dies
/// </summary>
public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI lifesLeft;
    public GameObject boardWood;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerDeath)
        {
            boardWood.SetActive(true);
            gameOver.SetActive(true);
        }
        else
        {
            gameOver.SetActive(false);
           // boardWood.SetActive(false);
            lifesLeft.text = "Balls remaining: " + GameManager.Instance.playerLifes + "\n Press Space to try again.";
        }
    }
}
