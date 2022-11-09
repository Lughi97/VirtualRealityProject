using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement and player beavhior
/// </summary>
public class PlayerWithPhysic : MonoBehaviour
{
    // add movement to player
    [Header("Player Movement")]
    private float horizontalInput;
    private float verticalInput;
    public float TopXZspeed = 30.0f;
    public float TopYspeed = -50f;
    public float force;
    private Vector3 pos;
    [SerializeField] private Vector2 movementInput;
    public bool endMaze = false;

    public float distanceTravelled = 0;
    public Vector3 lastPosition;
    RotationWorld rot;
    [SerializeField]
    private float anglex;
    [SerializeField]
    private float anglez;

    public bool isPlaying;

    public int lifes;
    //[SerializeField] private Vector3 forceSphere;

    Rigidbody rbPlayer;
    public float speedMultiplier;


    public List<GameObject> coinsCollected = new List<GameObject>();

    public PlayerType typePlayer;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody>();

        rbPlayer.velocity = new Vector3(0, 0, 0);
        lastPosition = transform.position;
        rot = gameObject.GetComponentInParent<RotationWorld>();
        //Physics.gravity = new Vector3(0, -f, 0);

        lifes = GameManager.Instance.playerLifes;

        TopXZspeed = typePlayer.maxXZVelocity;
        rbPlayer.mass = typePlayer.mass;
        speedMultiplier = typePlayer.speedSlopeBoost;

    }




    void FixedUpdate()
    {



        //Debug.Log(Physics.gravity);
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;



        // NEED TO FIND A SOLUTION TO THE PROBLEM FOR THE PLAYER MOVEMENT 
        changeVelocity();


        if (rbPlayer.velocity.magnitude > 1 && !isPlaying)
        {
            SFXManager.Instance.PlaySoundPlayer("RollingBallMainLoop", 0, true);
            isPlaying = true;
        }
        if (rbPlayer.velocity.magnitude <= 1)
        {
            SFXManager.Instance.stopSfxPlayer();
            isPlaying = false;
        }
        clampVelocity();
    }


    private void changeVelocity()
    {
        anglex = rot.rotX;
        anglez = rot.rotZ;
        float angle = rot.rotX + rot.rotZ;
        if (anglex > 5 && anglex <= 9)
        {
          //  Debug.Log("Slow FOWARD");
            rbPlayer.velocity += Vector3.forward / 5;
        }
        else if (anglex > 10 && anglex <= 15)
        {
           // Debug.Log("FAST FOWARD");
            rbPlayer.velocity += Vector3.forward * speedMultiplier;
        }

        if (anglex < -5 && anglex >= -8)
        {
         //   Debug.Log("Slow BACK");
            rbPlayer.velocity += Vector3.back / 5;
        }
        else if (anglex < -9 && anglex >= -15)
        {
          //  Debug.Log("FAST BACK");
            rbPlayer.velocity += Vector3.back * speedMultiplier;
        }
        if (anglez > 5 && anglez <= 9)
        {
         //   Debug.Log("Slow left");
            rbPlayer.velocity += Vector3.left / 5;
        }
        else if (anglez > 10 && anglez <= 15)
        {
            //Debug.Log("FAST left");
            rbPlayer.velocity += Vector3.left * speedMultiplier;
        }

        if (anglez < -5 && anglez >= -8)
        {
            //Debug.Log("Slow right");
            rbPlayer.velocity += Vector3.right / 5;
        }
        else if (anglez < -9 && anglez >= -15)
        {
          // Debug.Log("FAST right");
            rbPlayer.velocity += Vector3.right * speedMultiplier;
        }

    }
    private void clampVelocity()
    {
        //  Debug.Log (rbPlayer.velocity);
        if (rbPlayer.velocity.y < TopYspeed)
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, TopYspeed, rbPlayer.velocity.z);

        Vector2 tempXZvel = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.z);
        SFXManager.Instance.changePitchVelocity(rbPlayer.velocity.magnitude, 0f, TopXZspeed);

        if (tempXZvel.magnitude > TopXZspeed)
        {
            tempXZvel = tempXZvel.normalized * TopXZspeed;
            rbPlayer.velocity = new Vector3(tempXZvel.x, rbPlayer.velocity.y, tempXZvel.y);
        }


    }
    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up,2.5f);
     }
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.tag == "Pillar")
            {
                //Debug.Log("PILLAR");
                rbPlayer.AddForce(currentForce().magnitude * contact.normal, ForceMode.Impulse);
                collision.gameObject.GetComponent<Animator>().enabled = true;
                //collision.gameObject.GetComponent<Animator>().enabled = false;
                // collision.gameObject.GetComponent<ParticleSystem>().Play();
                string currentName = "HittingWood";
                int random = Random.Range(1, 7);
                playGameEffects(currentName, random);


            }
            if (collision.gameObject.tag == "Wall")
            {
                string currentName = "HitHardWood";
                int random = Random.Range(1, 5);
                playGameEffects(currentName, random);
                //  Debug.Log("WALL");
            }
            if (collision.gameObject.tag == "Coin")
                coinsCollected.Add(collision.gameObject);
            if (collision.gameObject.tag == "Hole")
            {
                string currentName = "PlayerDeath";
                playGameEffects(currentName, 0);
                GameManager.Instance.callCheckCorutne();
                //StartCoroutine(GameManager.Instance.checkStatusGame());
                GameManager.Instance.GameOver();
                // Debug.Log("LOSE LIFE");
                hidePlayer();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraPowerUp")
        {
            if (!ActivePower.powerCameraActive)
            {
                
                Debug.Log("HITPOWERCAMERA");
                 StartCoroutine(other.GetComponent<ShowFullMaze>().coolDown());
               
                other.gameObject.SetActive(false);
                string currentName = "CameraSnap";

                playGameEffects(currentName, 0);
            }

        }
        if (other.gameObject.tag == "ArrowPowerUp")
        {

            if (!ActivePower.powerArrowActive)
            {
                StartCoroutine(other.GetComponent<SpawnArrow>().coolDown());
                //other.GetComponent<SpawnArrow>().stratCoolDown();
                other.gameObject.SetActive(false);
                string currentName = "ArrowPower";


                playGameEffects(currentName, 0);
            }
                
               
          
        }
        else if (other.gameObject.tag == "Coin")
        {
            coinsCollected.Add(other.gameObject);
        }
        if (other.gameObject.tag == "SpeedUp")
        {
            Vector3 acceleration = other.GetComponent<SpeedUp>().Acceleration();
            Debug.Log("Acc: " + acceleration);
            TopXZspeed = 100f;
            rbPlayer.AddForce(acceleration, ForceMode.Impulse);
            Debug.Log(rbPlayer.velocity.magnitude);
            string currentName = "SpeedUp";
            int random = Random.Range(1, 4);
            playGameEffects(currentName, random);
            TopXZspeed = 30f;
            // other.gameObject.SetActive(false);
        }
        if (other.gameObject.name == "StartTrigger")
        {
            hidePlayer();
        }
        if (other.gameObject.tag == "Goal")
        {
            // Debug.Log("COINS TO COMPLETE: "+GameManager.Instance.LevelManagement.GetComponent<LevelManagement>().coinsLeftToComplete(coinsCollected.Count));

            if (GameManager.Instance.tmpLevelManager.GetComponent<LevelManagement>().coinsLeftToComplete(coinsCollected.Count) == 0)
            {
                playerFinishLevel();
                hidePlayer();
            }
            else
            {
                StartCoroutine(ScoreCurrentLevel.instance.notLevelComplete());
                Debug.Log("COIN LEFT: " + GameManager.Instance.tmpLevelManager.GetComponent<LevelManagement>().coinsLeft);
            }


        };
    }
    private Vector3 currentForce()
    {
        //Debug.Log(rbPlayer.mass * (rbPlayer.velocity)*2);
        return rbPlayer.mass * (rbPlayer.velocity) * 2;
    }

    private void hidePlayer()
    {

        this.gameObject.SetActive(false);
    }

    private void playGameEffects(string name, int randomIntSound)
    {
        if (name.Contains("Player"))
        {
            SFXManager.Instance.PlaySoundPlayer(name, 0f, false);


            // Debug.Log("LOSE LIFE");
        }
        if (randomIntSound != 0)
        {

            name = name + randomIntSound.ToString();

            if (name == "SpeedUp" + randomIntSound.ToString())
            {
                Debug.Log(name);
                SFXManager.Instance.Play(name, 0f, false);
            }
            SFXManager.Instance.Play(name, 0.5f, false);
        }
        else SFXManager.Instance.Play(name, 0, false);
    }

    private void playerFinishLevel()
    {
        //Debug.Log("YOU WIN");
        int score = (int)(distanceTravelled * 0.01f);
        // Debug.Log("TRAVEL SCORE: " + score);
        string currentName = "LevelComplete";
        GameManager.Instance.endLevel = true;
        playGameEffects(currentName, 0);
        MusicManager.Instance.StopAll();
        SFXManager.Instance.stopSfxPlayer();
        ScoreCurrentLevel.instance.CalculateCoins();
        ScoringSystem.Instance.getCurrentLevelDistanceScore(score);
        ScoringSystem.Instance.getCurrentLevelTotalScore();
        ScoreCurrentLevel.instance.ShowCurrentLevelScore();
        GameManager.Instance.callCheckCorutne();
        //StartCoroutine(GameManager.Instance.checkStatusGame());
    }
    public IEnumerator changeScale()
    {
        //Debug.Log(typePlayer.type +" "+ typePlayer.mass);
        yield return new WaitForSeconds(0.01f);
        TopXZspeed = typePlayer.maxXZVelocity;
        rbPlayer.mass = typePlayer.mass;
        speedMultiplier = typePlayer.speedSlopeBoost;
        transform.localScale = new Vector3(typePlayer.scale, typePlayer.scale, typePlayer.scale);
    }
}

