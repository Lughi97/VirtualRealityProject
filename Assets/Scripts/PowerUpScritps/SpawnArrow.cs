using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Create the arrow 
/// </summary>
public class SpawnArrow : PowerTemplate
{
    public GameObject arrow;
    [SerializeField]
    private GameObject tmpArrow;
    private float timer = 15f;
    private void Update()
    {
        checkStatusGame();
    }
    public override void checkStatusGame()
    {

        Debug.Log("Player is active= " + GameManager.Instance.tempPlayer.activeSelf);
        if (GameManager.Instance.isGameOver || GameManager.Instance.playerDeath || !GameManager.Instance.tempPlayer.activeSelf)
        {
            Debug.Log("DESTORYYYYYYYY");
            ActivePower.powerArrowActive = false;
            StopCoroutine(coolDown());
           
        }
    }
    public override IEnumerator coolDown()
    {
        tmpArrow = Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
        ActivePower.powerArrowActive = true;
        yield return new WaitForSeconds(timer);
        ActivePower.powerArrowActive = false;
        Destroy(tmpArrow);
    }

}
