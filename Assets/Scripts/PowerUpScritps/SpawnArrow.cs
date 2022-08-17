using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject arrow;
    [SerializeField]
    private GameObject tmpArrow;

    private void Update()
    {
       
    }
    public IEnumerator spawnArrow()
    {

        tmpArrow = Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
        //  StartCoroutine(fadeArrow());
        yield return new WaitForSeconds(15f);

        Destroy(tmpArrow);
    }

}
