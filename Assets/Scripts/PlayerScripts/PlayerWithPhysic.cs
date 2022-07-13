using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithPhysic : MonoBehaviour
{
    public float TopXZspeed=30.0f;
    public float TopYspeed = -50f;
    public float force = 10f;
    
    private Vector3 pos;
    private RotationWorld rotation;
    [SerializeField] private Vector2 movementInput;
    
    Rigidbody rbPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody>();
   
    }
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movementInput = new Vector2(moveHorizontal, moveVertical);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rbPlayer.AddForce(new Vector3(movementInput.x, 0, movementInput.y) * force);
        clampVelocity();
    }
    private void clampVelocity()
    {
        if (rbPlayer.velocity.y < TopYspeed)
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, TopYspeed, rbPlayer.velocity.z);
        Vector2 tempXZvel = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.z);
        if (tempXZvel.magnitude > TopXZspeed)
        {
            tempXZvel = tempXZvel.normalized * TopXZspeed;
            rbPlayer.velocity = new Vector3(tempXZvel.x, rbPlayer.velocity.y, tempXZvel.y);
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
           // Debug.Log(contact.normal);


            if (collision.gameObject.tag == "Pillar")
            {
                Debug.Log("PILLAR");
                //Vector3 force= new Vector3(Random.Range(1000f,2000f), 0f, Random.Range(1000f, 2000f));

                float force = Random.Range(3500f, 7500f);

                rbPlayer.AddForce(force * contact.normal);
               // Debug.Log(force * contact.normal);
               
            }
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("WALL");
            }
        }
        
    }



}
