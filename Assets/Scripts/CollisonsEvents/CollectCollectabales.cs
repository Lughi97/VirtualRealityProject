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
    [SerializeField] private string nameCoin;
    public bool bounce = false;

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
                contentScore = 3;
                nameCoin = "BronzeCoin";
                break;
            case TypeScore.silver:
                contentScore = 6;
                nameCoin = "silverCoin";
                break;
            case TypeScore.gold:
                contentScore = 9;
                nameCoin = "GoldCoin";
                break;

        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -100)
        { Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sendScoreToSystem();
           // Debug.Log("HIT");
            SFXManager.Instance.Play(nameCoin, 0, false);
            this.gameObject.SetActive(false);
            ScoreCurrentLevel.instance.AddCoin(this);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            
            Rigidbody rbCoin = this.gameObject.GetComponent<Rigidbody>();
            if (!bounce)
            {
                bounce = true;
                StartCoroutine(ResetTrigger());
            }


        }
        if (collision.gameObject.tag == "Player" && !this.gameObject.GetComponent<Collider>().isTrigger)
        {
            Debug.Log("Play");
            SFXManager.Instance.Play(nameCoin, 0, false);
            sendScoreToSystem();
          
            
            this.gameObject.SetActive(false);
            ScoreCurrentLevel.instance.AddCoin(this);
        }
    }

    IEnumerator ResetTrigger()
    {
        
        yield return new WaitForSeconds(5f);
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        transform.position = transform.position;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        StopCoroutine(ResetTrigger());

    }
    public void sendScoreToSystem()
    {
        //Debug.Log(contentScore);


    }
    public void AddRigidbody()
    {
        this.gameObject.AddComponent<Rigidbody>();
        Rigidbody rbCoin = this.gameObject.GetComponent<Rigidbody>();
    }

    public void RemoveAnimator()
    {
        Destroy(this.gameObject.GetComponent<Animator>());
    }

}
