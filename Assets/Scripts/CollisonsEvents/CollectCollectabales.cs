using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeScore
{
    coinStar,
    coin,
    star,

}
public class CollectCollectabales : MonoBehaviour
{

    public TypeScore typeScore;
    public int contentScore;
    /* public int ContentScore
      {
          get { return contentScore; }
          set { switch (typeScore)
              {
                  case TypeScore.coin:
                      contentScore = 10;
                      break;
                  case TypeScore.star:
                      contentScore = 30;
                      break;

              } }
      }
      */
    private void Start()
    {
        switch (typeScore)
        {
            case TypeScore.coin:
                contentScore = 10;
                break;
            case TypeScore.star:
                contentScore = 30;
                break;
            case TypeScore.coinStar:
                contentScore = 20;
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sendScoreToSystem();
            this.gameObject.SetActive(false);
        }
    }

    public void sendScoreToSystem()
    {
        Debug.Log(contentScore);
    }


}
