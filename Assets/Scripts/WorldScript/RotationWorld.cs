using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWorld : MonoBehaviour
{
    public float turnSpeed = 3.0f;
    [SerializeField]private float minRotation = -45;
    [SerializeField]private float maxRotation = 45;
    private float rotX;
    private float rotZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rotX += turnSpeed*Time.deltaTime;//this.transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.S))
            rotX += -turnSpeed* Time.deltaTime;//this.transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.A))
            rotZ += turnSpeed* Time.deltaTime;//this.transform.Rotate(Vector3.forward * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.D))
            rotZ += -turnSpeed*Time.deltaTime;//this.transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed);


        //if (currentRotation.x < 0) currentRotation.x = 360 + currentRotation.x;
        //if (currentRotation.z < 0) currentRotation.z = 360 + currentRotation.z;
        ;
        //  Debug.Log(currentRotation);
        rotX = Mathf.Clamp(rotX, minRotation, maxRotation);
        rotZ = Mathf.Clamp(rotZ, minRotation, maxRotation);

        transform.eulerAngles = new Vector3(rotX, 0f, rotZ);
    }


    void RotateWiiBalance()
    {
        if (Input.GetKey(KeyCode.W))
            rotX += turnSpeed * Time.deltaTime;//this.transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.S))
            rotX += -turnSpeed * Time.deltaTime;//this.transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.A))
            rotZ += turnSpeed * Time.deltaTime;//this.transform.Rotate(Vector3.forward * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.D))
            rotZ += -turnSpeed * Time.deltaTime;//this.transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed);


        //if (currentRotation.x < 0) currentRotation.x = 360 + currentRotation.x;
        //if (currentRotation.z < 0) currentRotation.z = 360 + currentRotation.z;
        ;
        //  Debug.Log(currentRotation);
        rotX = Mathf.Clamp(rotX, minRotation, maxRotation);
        rotZ = Mathf.Clamp(rotZ, minRotation, maxRotation);

        transform.eulerAngles = new Vector3(rotX, 0f, rotZ);
    }
       
}
