using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Show the full maze for top view
/// </summary>
public class ShowFullMaze : PowerTemplate
{

    public Camera mainCamera;
    [SerializeField]
    private GameObject mainCameraObj;
    [SerializeField]
    private GameObject powerUpCamera;
    public Camera power;
    public float timer = 5;

    void Start()
    {
        mainCamera = GameManager.Instance.tmpCamera;
        if (mainCameraObj != null)
            mainCamera.enabled = true;
        if (powerUpCamera != null)
            powerUpCamera.SetActive(false);
    }

    void Update()
    {
        checkStatusGame();
    }
    public override void checkStatusGame()
    {
        if (powerUpCamera == null)
        {
            powerUpCamera = GameObject.Find("ShowFullMaze(Clone)");
            // Debug.Log(powerUpCamera);
            if (power == null)
            {
                power = powerUpCamera.GetComponent<Camera>();
                power.enabled = false;
            }
        }
        if (mainCameraObj == null)
        {
            mainCameraObj = GameObject.Find("Main Camera(Clone)");
            // Debug.Log(powerUpCamera);
            if (mainCamera == null)
            {
                mainCamera = mainCameraObj.GetComponent<Camera>();
                mainCamera.enabled = true;

            }
        }

        if (GameManager.Instance.endLevel || GameManager.Instance.playerDeath || GameManager.Instance.isGameOver)
        {
            Debug.Log("THIS IS TEST TO Stop the powerup");
            mainCamera.enabled = true;
            power.enabled = false;
            StopCoroutine(coolDown());
        }
        //throw new System.NotImplementedException();
    }
    public override IEnumerator coolDown()
    {
        ActivePower.powerCameraActive = true;
        mainCamera.enabled = !mainCamera.enabled;

        power.enabled = !power.enabled;

        yield return new WaitForSeconds(timer);
        ActivePower.powerCameraActive = false;
        Debug.Log("HEY");
        mainCamera.enabled = !mainCamera.enabled;
        power.enabled = !power.enabled;



    }
}
