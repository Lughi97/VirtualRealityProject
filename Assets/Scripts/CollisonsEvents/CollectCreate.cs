using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCreate : MonoBehaviour
{
    public GameObject []coins;
    public int coinsContained = 5;
    public GameObject destoryedCreate;
    
    // Start is called before the first frame update
    void Start()
    {
        // start animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Destory Create");
            GameObject temp=Instantiate(destoryedCreate, transform.position, transform.rotation);
            temp.transform.parent = this.gameObject.transform.parent;
            Destroy(this.gameObject);
           
            collectCoins(temp);

        }
    }

   // public void FadeBroken()
 //   {
  //      Debug.Log("HEEEEEY");
  //      Transform child = gameObject.transform.GetChild(0);
   //     Destroy(child.gameObject);
  //  }
    private void collectCoins(GameObject broken)
    {
        Invoke("FadeBroken", 3.0f);
        for (int x=0; x<= coinsContained; x++)
        {
            int splash = Random.Range(0, 10);
            Vector3 direction = new Vector3(Random.Range(-splash, splash) * Mathf.Cos(Random.Range(0,Mathf.PI)), 5, Random.Range(-splash, splash) * Mathf.Sin(Random.Range(0, Mathf.PI)));
           // Debug.Log(direction);
            spawnCoin(broken, direction);
        }
       

    }


    private void spawnCoin(GameObject broken, Vector3 direction)
    {
      
        switch (Random.Range(0, 3))
        {
            case 0:
                GameObject coinB = Instantiate(coins[0], new Vector3(transform.position.x, 5, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                coinB.transform.parent = broken.transform.parent;
                Transform coinBronze = coinB.transform.GetChild(0);
                makeCoinDrop(coinBronze, direction);

                break;
            case 1:
                GameObject coinS = Instantiate(coins[1], new Vector3(transform.position.x, 5, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                coinS.transform.parent = broken.transform.parent;
                Transform coinSilver = coinS.transform.GetChild(0);
                makeCoinDrop(coinSilver, direction);

                break;
            case 2:
                GameObject coinG = Instantiate(coins[2], new Vector3(transform.position.x, 5, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                coinG.transform.parent = broken.transform.parent;
                Transform coinGold = coinG.transform.GetChild(0);
                makeCoinDrop(coinGold, direction);

                break;


        }

    }


    private void makeCoinDrop(Transform coin, Vector3 direction)
    {
        
        coin.GetComponent<CollectCollectabales>().AddRigidbody();
        coin.GetComponent<CollectCollectabales>().RemoveAnimator();
        coin.GetComponent<Collider>().isTrigger = false;
        Rigidbody rbCoin = coin.GetComponent<Rigidbody>();
        rbCoin.mass = 3;
        rbCoin.AddForce(direction, ForceMode.Impulse);
    }

  
}
