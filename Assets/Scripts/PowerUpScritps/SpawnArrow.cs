using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject arrow;
    [SerializeField]
    private GameObject tmpArrow;
    private bool active;
    private float timer = 15f;
    private void Update()
    {
       
    }
    public IEnumerator spawnArrow()
    {
        if (!active)
        {
            tmpArrow = Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
            active = true;
        }
        else { timer = 15f; }
        yield return new WaitForSeconds(timer);
        active = false;
        Destroy(tmpArrow);
    }

}
