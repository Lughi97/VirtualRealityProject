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
            StartCoroutine(FadeBroken(temp));
            Destroy(gameObject);

        }
    }
    private IEnumerator FadeBroken(GameObject broken)
    {
       
            yield return new WaitForSeconds(5f);
            Destroy(broken);


        
    }
    private void collectCoins()
    {
        int random= Random.Range(0,10);
        for( int x=0; x< coinsContained; x++)
        {
           
        }
    }
}
