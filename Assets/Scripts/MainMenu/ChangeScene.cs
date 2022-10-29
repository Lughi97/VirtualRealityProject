using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Main mechanic to change between scene and menu scenes
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public GameObject canvas;

    [SerializeField] private GameObject board;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        canvas = GameObject.Find("MainMenuCanvas");
        board = canvas.transform.Find("WoodBoardMenu").gameObject;
        panel= canvas.transform.Find("CrossFade").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            switch (gameObject.name)
            {
                case "StartTrigger":
                    start();
                    break;
                case "LevelTrigger":
                    GameManager.Instance.typeScene = SceneLevel.HighScoreMenu;
                    StartCoroutine(GameManager.Instance.changeMenuScene());
                    break;
                case "CreditsTrigger":
                    GameManager.Instance.typeScene = SceneLevel.Credits;
                    StartCoroutine(GameManager.Instance.changeMenuScene());
                    break;
                case "ShopTrigger":
                    GameManager.Instance.typeScene = SceneLevel.Shop;
                    StartCoroutine(GameManager.Instance.changeMenuScene());
                    break;
                case "ReturnMenuTrigger":
                    GameManager.Instance.typeScene = SceneLevel.MainMenu;
                    StartCoroutine(GameManager.Instance.changeMenuScene());
                    break;
                case "QuitTrigger":
                    Debug.Log("QUIT GAME");
                    Application.Quit();
                    break;

            }
        }
    }
    void start()
    {
        GameManager.Instance.typeScene = SceneLevel.Level1;
        GameManager.Instance.mainMenuLevel = false;
        SFXManager.Instance.StopAll();
        MusicManager.Instance.StopAll();
        GameManager.Instance.isLoaded = false;
        //Debug.Log("CHANGE SCENE IN 2 SECONDS");
        board.SetActive(true);
        //yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene("SampleScene");
        LoadGame.getBoard(board,panel.GetComponent<Animator>());
        StartCoroutine(LoadGame.LoadScene("SampleScene"));
    }
}
