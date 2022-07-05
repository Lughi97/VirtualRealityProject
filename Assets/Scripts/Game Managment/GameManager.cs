using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeSpawner maze;

    private MazeSpawner mazeInstance;

    public GameObject Ground;
   
    void Start()
    {
        startGame();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) restartGame();
    }
    private void startGame()
    {
        Debug.Log("StartGame");
        mazeInstance = Instantiate(maze) as MazeSpawner;
        mazeInstance.transform.parent = Ground.transform;
       // mazeInstance.generateGround();
        //StartCoroutine(mazeInstance.Generate());
    }
    private void restartGame()
    {
        //StopAllCoroutines();
        Debug.Log("Restart");
        Destroy(mazeInstance.gameObject);
        startGame();
    }
}
