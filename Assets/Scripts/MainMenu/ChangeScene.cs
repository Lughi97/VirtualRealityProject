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
                    goToNewScene();
                    break;
                case "SettingTrigger":
                    GameManager.Instance.typeScene = SceneLevel.Settings;
                    goToNewScene();
                    break;
                case "ShopTrigger":
                    GameManager.Instance.typeScene = SceneLevel.Shop;
                   goToNewScene();
                    break;
                case "ReturnMenuTrigger":
                    GameManager.Instance.typeScene = SceneLevel.MainMenu;
                    goToNewScene();
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
    void goToNewScene()
    {
        // Destroy(GameManager.Instance.tmpCamera.gameObject);
        //Debug.Log("CHANGE SCENE IN 2 SECONDS");
        //yield return new WaitForSeconds(2f);
        // GameManager.Instance.isLoaded = false;
        
        GameManager.Instance.changeMenuScene();
        //SceneManager.LoadScene("SampleScene");
    }
}
