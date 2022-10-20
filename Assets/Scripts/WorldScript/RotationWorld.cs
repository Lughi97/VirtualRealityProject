using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotation of the ground of the game 
/// </summary>
public class RotationWorld : MonoBehaviour
{
    public float turnSpeed = 3.0f;
    [SerializeField] private float minRotation = -45;
    [SerializeField] private float maxRotation = 45;
    public float rotX;
    public float rotZ;
   // public float angle;
    public Vector3 currentRot;
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        RotateWiiBalance();
        currentRot = GetComponent<Transform>().eulerAngles;
    }


    void RotateWiiBalance()
    {
        if (Input.GetKey(KeyCode.W))
            rotX += turnSpeed * Time.fixedDeltaTime;//this.transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.S))
            rotX += -turnSpeed * Time.fixedDeltaTime;//this.transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.A))
            rotZ += turnSpeed * Time.fixedDeltaTime;//this.transform.Rotate(Vector3.forward * Time.deltaTime * turnSpeed);
        if (Input.GetKey(KeyCode.D))
            rotZ += -turnSpeed * Time.fixedDeltaTime;//this.transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed);

        rotX = Mathf.Clamp(rotX, minRotation, maxRotation);
        rotZ = Mathf.Clamp(rotZ, minRotation, maxRotation);
        
        transform.eulerAngles = new Vector3(rotX, 0f, rotZ);
        ///angle = rotX + rotZ;
    
    }
    
}
