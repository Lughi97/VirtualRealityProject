using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithPhysic : MonoBehaviour
{
    // add movement to player
    public float TopXZspeed=30.0f;
    public float TopYspeed = -50f;
    public float force = 10f;
    private Vector3 pos;
    private RotationWorld rotation;
    [SerializeField] private Vector2 movementInput;
    public bool endMaze = false;
    public bool plDeath = false;
    //[SerializeField] private Vector3 forceSphere;
    
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
        
        //Debug.DrawLine(rbPlayer.velocity.normalized,rbPlayer.velocity.normalized+new Vector3(1,1,1));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Debug.Log(forceSphere.magnitude);
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
            if (collision.gameObject.tag == "Pillar")
            {
                //Debug.Log("PILLAR");
                rbPlayer.AddForce(currentForce().magnitude * contact.normal,ForceMode.Impulse);
               
               // collision.gameObject.GetComponent<ParticleSystem>().Play();
               
            }
            if (collision.gameObject.tag == "Wall")
            {
              //  Debug.Log("WALL");
            }
            if (collision.gameObject.tag == "Hole")
            {
               // Debug.Log("LOSE LIFE");
                hidePlayer();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal") {
            Debug.Log("YOU WIN");
            ScoreCurrentLevel.instance.CalculateCoins();
            hidePlayer();
            };
    }
    private Vector3 currentForce()
    {
        return rbPlayer.mass * (rbPlayer.velocity);
    }

    private void hidePlayer()
    {
        this.gameObject.SetActive(false);
    }
}
