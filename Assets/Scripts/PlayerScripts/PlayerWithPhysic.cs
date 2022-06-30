using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithPhysic : MonoBehaviour
{
    //public float speedPlayer=3.0f;
    public GameObject world;
    //public float sphereradious= 2.5f;
   // public float currentPositionY;
   // private float initialPositionY;
    // Start is called before the first frame update
    void Start()
    {
        //rbPlayer = this.GetComponent<Rigidbody>();
        //initialPositionY = transform.position.y + sphereradious;
        //currentPositionY = transform.position.y;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        /*This is the movement of the player with Physic!
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);


        if ( rbPlayer.velocity.magnitude<= 20.0f)
        {
        Debug.Log(rbPlayer.velocity.magnitude);
        rbPlayer.AddForce(playerMovement * speedPlayer);
         }
         */
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            Debug.Log("hello");
            transform.parent =world.transform ;
        }
            
    }
    // private void OnTriggerExit(Collider other)
    //{
    //  if (other.gameObject.tag == "ground")
    //    transform.parent = null;
    //}


}
