using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFullMaze : MonoBehaviour
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

        if (GameManager.Instance.restartLevel == true)
        {
            StopAllCoroutines();
        }
    }



    public IEnumerator powerUpCoolDown()
    {
        mainCamera.enabled = !mainCamera.enabled;

        power.enabled = !power.enabled;

        yield return new WaitForSeconds(timer);
        Debug.Log("HEY");
        mainCamera.enabled = !mainCamera.enabled;
        power.enabled = !power.enabled;



    }
}
