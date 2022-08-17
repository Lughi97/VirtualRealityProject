using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public Vector3 Direction;
    public float acceleration;
    public bool used = false;
    public Renderer rend;
    [SerializeField]
    private Color colorSpeedUp,colorSpeedOff;
    
    private void Start()
    {
        rend = GetComponent<Renderer>();
        colorSpeedUp = rend.material.color;
        //colorSpeedOff = new Color(196, 30, 58);
    }
    private void Update()
    {
        Transform parent = gameObject.transform.parent;
        Debug.Log(gameObject.transform.parent.localEulerAngles.y);
       
    }
    public Vector3 Acceleration()
    {
        if (!used)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(charge());
        }
        //float value= UnityEditor.TransformUtils.GetInspectorRotation(parent).y;
        switch (gameObject.transform.parent.localEulerAngles.y)
        {
            case 0:
                return Direction = new Vector3(-acceleration, 0, 0f);
            case 90:
                return Direction = new Vector3(0, 0, acceleration);
            case 180:
                return Direction = new Vector3(acceleration, 0, 0);
            case 270:
                return Direction = new Vector3(0, 0, -acceleration);
            default:
                return Direction = new Vector3(0, 0, acceleration);

        }
       
    }
    IEnumerator charge()
    {
        used = true;
        rend.material.color = colorSpeedOff;
        yield return new WaitForSeconds(5f);
        rend.material.color = colorSpeedUp;
        used = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
