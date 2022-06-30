using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Maze mazePrefab;

    private Maze mazeInstance;
   
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
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.generateGround();
        //StartCoroutine(mazeInstance.Generate());
    }
    private void restartGame()
    {
        StopAllCoroutines();
        Debug.Log("Restart");
        Destroy(mazeInstance.gameObject);
        startGame();
    }
}
