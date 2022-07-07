using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeSpawner maze;

    private MazeSpawner mazeInstance;

    public GameObject Ground;
    public GameObject Player;
    private int setUpTime = 1;
    void Start()
    {
        startGame();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) restartGame();
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

        Debug.Log(mazeInstance.transform.position);
        Ground.transform.position = new Vector3(((mazeInstance.CellWidth * mazeInstance.Rows) / 2) - 5, 0, ((mazeInstance.CellHeight * mazeInstance.Columns) / 2) - 5);
        mazeInstance.transform.parent = Ground.transform;
     
        Player.transform.position = new Vector3(mazeInstance.transform.position.x,1, mazeInstance.transform.position.z);
        Player.transform.parent = Ground.transform;

        // mazeInstance.generateGround();
        //StartCoroutine(mazeInstance.Generate());
    }
}
