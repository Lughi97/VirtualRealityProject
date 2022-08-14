using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCreate : MonoBehaviour
{
    public GameObject []coins;
    public int coinsContained = 5;
    public GameObject destoryedCreate;
    public bool destroyedCreate = false;
    
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
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            collectCoins(temp);
            StartCoroutine(fade(temp));

        }
    }
    IEnumerator fade(GameObject temp)
    {
        destroyedCreate = true;
        while (destroyedCreate)
        {
            
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(5);

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            Destroy(gameObject);
            Destroy(temp);
            
            destroyedCreate = false;
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
      
        for (int x=0; x<= coinsContained; x++)
        {
            int splash = Random.Range(0, 10);
            Vector3 direction = new Vector3( Mathf.Cos(Random.Range(0,Mathf.PI)),1, Mathf.Sin(Random.Range(0, Mathf.PI)));
          // Debug.Log(direction);
            spawnCoin(broken, direction);
        }
       

    }


    private void spawnCoin(GameObject broken, Vector3 direction)
    {
      
        switch (Random.Range(0, 3))
        {
            case 0:
                GameObject coinB = Instantiate(coins[0], new Vector3(broken.transform.position.x, 3, broken.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                coinB.transform.parent = broken.transform.parent;
                if (coinB.transform.position.y < 0f)
                    coinB.transform.position = new Vector3(broken.transform.position.x, 3, broken.transform.position.z);
                Transform coinBronze = coinB.transform.GetChild(0);
                makeCoinDrop(coinBronze, direction);

                break;
            case 1:
                GameObject coinS = Instantiate(coins[1], new Vector3(transform.position.x, 3, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
             //   Debug.Log(coinS.transform.position);
                coinS.transform.parent = broken.transform.parent;
                if (coinS.transform.position.y < 0f)
                    coinS.transform.position = new Vector3(broken.transform.position.x, 3, broken.transform.position.z);
                Transform coinSilver = coinS.transform.GetChild(0);
                makeCoinDrop(coinSilver, direction);

                break;
            case 2:
                GameObject coinG = Instantiate(coins[2], new Vector3(transform.position.x, 3, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                coinG.transform.parent = broken.transform.parent;
               // Debug.Log(coinG.transform.position);
                if (coinG.transform.position.y < 0f)
                    coinG.transform.position = new Vector3(broken.transform.position.x, 3, broken.transform.position.z);
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
       // rbCoin.AddForce(direction, ForceMode.Impulse);
    }

  
}
