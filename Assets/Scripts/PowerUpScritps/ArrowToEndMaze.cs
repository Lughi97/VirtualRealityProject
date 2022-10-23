using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Functionality of the Arrow powerup that shows the direction of the goal
/// </summary>
public class ArrowToEndMaze : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    public GameObject lookAtTarget { get { return _target; } }
    Vector3 Direction;


    public GameObject player;
    private Vector3 offset;
    private float height = 10f;
    public float distance = 2f;

    [SerializeField]
    private Transform _arrow;
    public Transform Arrow { get { return _arrow; } }

    public float distToTarget;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {

        if (!GameManager.Instance.tempPlayer.activeSelf)
        {
            Debug.Log("HELLO THIS IS THE Destruction of temp arrow");
            Destroy(gameObject);

        }
        if (lookAtTarget != null)
        {
            
           // Debug.Log(lookAtTarget);
            Vector3 difference = lookAtTarget.transform.position - transform.position;
            float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
        if (Arrow != null)
        {
           // Debug.Log(Arrow);
            distToTarget = Vector3.Distance(lookAtTarget.transform.position, transform.position);
            speed = chooseSpeed(distToTarget);
            // Debug.Log(distToTarget);
            Arrow.transform.Rotate(speed * Time.fixedDeltaTime, 0, 0);
        }
        else getArrow();
        getPlayer();
        getGoal();
    }

    private float chooseSpeed(float dist)
    {

        if (distToTarget > 150)
            return speed;
        if (distToTarget > 100 && distToTarget < 150)
            return speed = 15f;
        if (distToTarget > 50 && distToTarget < 100)
            return speed = 25f;
        if (distToTarget > 25 && distToTarget < 50)
            return speed = 50f;
        else if (distToTarget < 25)
            return 75f;
        else
            return 0f;
    }

    private void setTarget(GameObject target = null)
    {
        _target = target;
    }

    private void getArrow()
    {
        _arrow = transform.GetChild(0);
    }
    private void getPlayer()
    {

        if (player == null)
        {

            player = GameManager.Instance.tempPlayer;
            //player = GameObject.Find("Player(Clone)");
            //this.gameObject.transform.parent = player.transform;

            offset = transform.position - player.transform.position;
            transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z);

        }
        else if (player.activeSelf == false)
        {
            transform.position = this.transform.position;
        }
        else
        {

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z + distance);

        }

    }
    public void getGoal()
    {
        if (_target == null)
            _target = GameObject.Find("Goal(Clone)");

    }
}
