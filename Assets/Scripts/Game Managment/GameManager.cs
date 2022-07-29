using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeSpawner maze;

    private MazeSpawner mazeInstance;

    public GameObject Ground;
    private GameObject tempGround;
    public GameObject Player;
    public GameObject tempPlayer;

    public Camera playerCamera;
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
        setUpMazeLevel();
  
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


    public void setUpMazeLevel()// here we build the maze in base of the level (to expand)
    {
        buildmaze();
        addPlayer();
    }

    private void restartGame()
    {
        
        Debug.Log("Restart");
        Destroy(mazeInstance.gameObject);
        Destroy(tempGround);
        Destroy(tempPlayer);
        setUpMazeLevel();
    }
    private void buildmaze()
    {
        Debug.Log("StartGame");
        mazeInstance = Instantiate(maze) as MazeSpawner;
        tempGround = Instantiate(Ground, new Vector3(((mazeInstance.CellWidth * mazeInstance.Rows) / 2) - 5, 0, ((mazeInstance.CellHeight * mazeInstance.Columns) / 2) - 5), Quaternion.Euler(0, 0, 0)) as GameObject;
        mazeInstance.transform.parent = tempGround.transform;
        playerCamera.transform.position = new Vector3(transform.position.x, playerCamera.gameObject.GetComponent<FollowPlayer>().height, transform.position.z);

    }

    public void addPlayer()
    {
        tempPlayer = Instantiate(Player, new Vector3(mazeInstance.transform.position.x, 2, mazeInstance.transform.position.z), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        tempPlayer.transform.parent = tempGround.transform;
    }
    

    public void resetGround()
    {
       
        if (tempPlayer.activeSelf == false)
        {
            tempGround.GetComponent<RotationWorld>().enabled = false;
            Debug.Log("HERE");
        }
    }

    
    //private void nextLevel() { };
   // private void returnMenu() { };
}
