using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeSpawner maze;

    private MazeSpawner mazeInstance;

    public GameObject Ground;
    public GameObject Player;
    public GameObject tempPlayer;
    [SerializeField]public static GameManager instance = null;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        startGame();
  
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) restartGame();
        if (tempPlayer == null)
        {
            tempPlayer = GameObject.Find("Player(Clone)");
        }
        else
        {
            resetGround();
        }
    }

    private void restartGame()
    {
        //StopAllCoroutines();
        Debug.Log("Restart");
        Destroy(mazeInstance.gameObject);
        startGame();
    }
    private void startGame()
    {
        Debug.Log("StartGame");
        mazeInstance = Instantiate(maze) as MazeSpawner;
       
      //  Debug.Log(mazeInstance.transform.position);
        Ground.transform.position = new Vector3(((mazeInstance.CellWidth * mazeInstance.Rows) / 2) - 5, 0, ((mazeInstance.CellHeight * mazeInstance.Columns) / 2) - 5);
        Ground.transform.localRotation = Quaternion.Euler(0, 0, 0);
        mazeInstance.transform.parent = Ground.transform;
        addPlayer();

    }

    public void addPlayer()
    {
        tempPlayer = Instantiate(Player, new Vector3(mazeInstance.transform.position.x, 2, mazeInstance.transform.position.z), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        tempPlayer.transform.parent = Ground.transform;
    }
    //if(player.GetComponent<PlayerWithPhysic>().endMaze==true || player.GetComponent<PlayerWithPhysic>().plDeath == true)
    //    {
      //      player = null;
        //}

    public void resetGround()
    {
        Debug.Log("HERE");
        if (tempPlayer.GetComponent<PlayerWithPhysic>().endMaze == true|| tempPlayer.GetComponent<PlayerWithPhysic>().plDeath == true)
        {
            Ground.GetComponent<RotationWorld>().turnSpeed = 0f;
        }
    }
    //private void nextLevel() { };
   // private void returnMenu() { };
}
