using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeScore
{
    bronze,
    silver,
    gold,

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
            case TypeScore.bronze:
                contentScore = 12;
                break;
            case TypeScore.silver:
                contentScore = 25;
                break;
            case TypeScore.gold:
                contentScore = 50;
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sendScoreToSystem();

            this.gameObject.SetActive(false);
            ScoreCurrentLevel.instance.AddCoin(this);
        }
    }

    public void sendScoreToSystem()
    {
        Debug.Log(contentScore);
        
        
    }


}
