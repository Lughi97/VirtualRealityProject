using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game management: create the game level, change level, check in which level is the game, 
/// currently and if the level can be completed
/// </summary>
public enum SceneLevel
{
    TitleCard,
    MainMenu,
    HighScoreMenu,
    Shop,
    Settings,
    Tutorial,
    Level1,
    Level2,
    Level3
}
public class GameManager : Singleton<GameManager>
{

    [Header("Game Management")]

    [Header("Level Management")]
    //Selecet Scene
    public SceneLevel typeScene;
    public bool mainMenuLevel;
    public int mazeRows;
    public int mazeColums;
    public int currentLevel;
    [SerializeField]
    public GameObject LevelManagement;
    public GameObject tmpLevelManager;


    [Header("Level")]
    //SingleLevel
    public MazeSpawner maze;
    private MazeSpawner mazeInstance;
    public GameObject Ground;
    public GameObject tempGround;
    public GameObject Player;
    public GameObject tempPlayer;
    public bool restartLevel = false;
    public bool isLoaded = false;
    public bool endLevel = false;
    public bool isGameOver = false;
    public bool playerDeath = false;
    private string nameMusic;
    [SerializeField] private GameObject canvas;
    public GameObject crossFade;


    [Header("Player")]
    public List<PlayerType> skinsPlayer = new List<PlayerType>();
    [SerializeField] private PlayerType currentSkin;
    public GameObject currentPlayerSkin;
    public Camera playerCamera;
    public Camera tmpCamera;
    public GameObject powerUpCamera;
    private GameObject tempCameraPower;
    private float heightPowerCamera;
    public int playerLifes = 3;

    
    //[SerializeField] public static GameManager instance = null;
    private void Awake()
    {
        if (FindObjectsOfType(typeof(SFXManager)).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        isLoaded = false;

    }
    void Start()
    {
        sceneType();
        if (SaveSystem.loadType() != null)
            currentSkin = SaveSystem.loadType();
        else
            currentSkin = skinsPlayer[0];
        //startGame();
        // MusicManager.Instance.Play(nameMusic, 0, true);
        StartCoroutine(waitForTitle());
    }
    private IEnumerator waitForTitle()
    {
        Debug.Log("START ANIMATION");
        yield return new WaitForSeconds(6f);
        StartCoroutine(LoadGame.loadToMainMenu(crossFade.GetComponent<Animator>()));
    }
    public void sceneType()
    {
        switch (typeScene)
        {
            case SceneLevel.TitleCard:
                /// nameMusic = "MainMenuMusic";
                canvas = GameObject.Find("TitleCardCanvas");

                crossFade = canvas.transform.Find("CrossFade").gameObject; ;
                if (SaveSystem.LoadHighScores() != null)
                {
                    // Debug.Log("HELLO SCORES");
                    ScoringSystem.Instance.HighScoreLevel1 = SaveSystem.LoadHighScores()[0];
                    ScoringSystem.Instance.HighScoreLevel2 = SaveSystem.LoadHighScores()[1];
                    ScoringSystem.Instance.HighScoreLevel3 = SaveSystem.LoadHighScores()[2];

                }
                if (SaveSystem.LoadCoinCollected() != null)
                {
                    //Debug.Log("HELLO COINS");
                    ScoringSystem.Instance.listCoins = SaveSystem.LoadCoinCollected();
                }
                break;
            case SceneLevel.MainMenu:
                //currentSkin = skinsPlayer[0];
                mainMenuLevel = true;
                mazeRows = 3;
                mazeColums = 3;
                nameMusic = "MainMenuMusic";

                if (SaveSystem.LoadHighScores() != null)
                {
                    //  Debug.Log("HELLO SCORES");
                    ScoringSystem.Instance.HighScoreLevel1 = SaveSystem.LoadHighScores()[0];
                    ScoringSystem.Instance.HighScoreLevel2 = SaveSystem.LoadHighScores()[1];
                    ScoringSystem.Instance.HighScoreLevel3 = SaveSystem.LoadHighScores()[2];

                }
                if (SaveSystem.LoadCoinCollected() != null)
                {
                    //Debug.Log("HELLO COINS");
                    ScoringSystem.Instance.listCoins = SaveSystem.LoadCoinCollected();
                }
                canvas = GameObject.Find("MainMenuCanvas");

                crossFade = canvas.transform.Find("CrossFade").gameObject;

                //load MainMenuScene
                break;
            case SceneLevel.HighScoreMenu:
                {

                    mainMenuLevel = true;
                    mazeRows = 3;
                    mazeColums = 3;
                    nameMusic = "MainMenuMusic";
                    heightPowerCamera = 100;
                    break;
                }
            case SceneLevel.Level1:
                //loadLevel1
                canvas = GameObject.Find("LevelCanvas");

                crossFade = canvas.transform.Find("CrossFade").gameObject;
                currentLevel = 1;
                mazeRows = 4;
                mazeColums = 4;
                nameMusic = "Level1Music";
                heightPowerCamera = 100;
                break;
            case SceneLevel.Level2:
                canvas = GameObject.Find("LevelCanvas");

                crossFade = canvas.transform.Find("CrossFade").gameObject;
                currentLevel = 2;
                mazeRows = 6;
                mazeColums = 6;
                nameMusic = "Level2Music";
                heightPowerCamera = 200;
                //loadLevel2
                break;
            case SceneLevel.Level3:
                canvas = GameObject.Find("LevelCanvas");

                crossFade = canvas.transform.Find("CrossFade").gameObject;
                //loadLEvel3
                currentLevel = 3;
                mazeRows = 8;
                mazeColums = 8;
                heightPowerCamera = 300;
                nameMusic = "Level3Music";
                break;

        }
    }

    public void startGame()
    {
        //isLoaded = true;

        setUpMazeLevel();
        //to comment this is were the titleCard is lounced


        //   LoadGame.loadToMainMenu(crossFade.GetComponent<Animator>());
    }


    void Update()
    {
        checkCurrentScene();

        if (!mainMenuLevel)
        {

            if (endLevel && (Input.GetKeyDown(KeyCode.Space)))
            {

                Debug.Log("SAVE");
                SaveSystem.SaveScore(ScoringSystem.Instance);
                StartCoroutine(LoadGame.load(crossFade.GetComponent<Animator>()));
                // nextLevel();

            }
            else if (isGameOver && (Input.GetKeyDown(KeyCode.Space)))
            {
                Debug.Log("SAVE");
                Debug.Log("END GAME IS HERE");
                //LoadGame.load(crossFade.GetComponent<Animator>());
                returnMenu();


            }
            if (tempPlayer == null)
            {
                tempPlayer = GameObject.Find("Player(Clone)");
            }
            else
            {
                resetGround();
            }
        }

    }


    public void setUpMazeLevel()// here we build the maze in base of the level (to expand)
    {
        buildmaze();
        addPlayer();
    }

    private void restartGame()
    {
        MusicManager.Instance.StopAll();
        Destroy(tmpCamera.gameObject);
        Destroy(tmpLevelManager);
        Debug.Log("Restart");
        restartLevel = true;
        // endLevel = false;
        playerDeath = false;
        ScoringSystem.Instance.resetCurrentLevelScore();
        ScoreCurrentLevel.instance.LevelComplete.gameObject.SetActive(false);
        ScoreCurrentLevel.instance.coinCanvas.gameObject.SetActive(true);
        StartCoroutine(Restart());
        Destroy(mazeInstance.gameObject);
        Destroy(tempGround);
        Destroy(tempPlayer);

        setUpMazeLevel();

    }

    public void changeMenuScene()
    {
        Destroy(mazeInstance.gameObject);
        Destroy(tempGround);
        Destroy(tempPlayer);
        Destroy(tmpCamera.gameObject);
        Destroy(tmpLevelManager);




        setUpMazeLevel();

    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        MusicManager.Instance.Play(nameMusic, 0, true);
        // MusicManager.Instance.Play("Level1Background", 0, true);
        // MusicManager.Instance.PlayMusic("Level1Music", true, 384.6f, 5);
        // MusicManager.Instance.PlayMusic("Level1Background", true, 123f, 15);
        restartLevel = false;

    }
    private void buildmaze()
    {
        //Debug.Log("StartGame");
        mazeInstance = Instantiate(maze) as MazeSpawner;
        // Debug.Log(new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, 0, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2));
        tempGround = Instantiate(Ground, new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, 0, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2), Quaternion.Euler(0, 0, 0)) as GameObject;
        mazeInstance.transform.parent = tempGround.transform;
        tmpCamera = Instantiate(playerCamera, playerCamera.transform.position, playerCamera.transform.rotation) as Camera;
        tmpCamera.transform.position = new Vector3(transform.position.x, playerCamera.gameObject.GetComponent<FollowPlayer>().height, transform.position.z);
        if (!mainMenuLevel)
        {
            tmpLevelManager = Instantiate(LevelManagement, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

        }
        if (tempCameraPower == null)
        {
            //Debug.Log("TEMP CAMERA");
            tempCameraPower = Instantiate(powerUpCamera, new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, heightPowerCamera, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2), Quaternion.Euler(90, 0, 0)) as GameObject;
            tempCameraPower.GetComponent<Camera>().enabled = false;
        }
        else tempCameraPower.transform.position = new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, heightPowerCamera, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2);

    }

    public void addPlayer()
    {
        Debug.Log("ADD PLAYER");
        if (!mainMenuLevel)
        {
            tempPlayer = Instantiate(Player, new Vector3(mazeInstance.transform.position.x, 2, mazeInstance.transform.position.z), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            tempPlayer.transform.parent = tempGround.transform;
            Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer);
            tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer = currentSkin;
            Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer + "  " + tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer.mass);
            tempPlayer.GetComponent<PlayerWithPhysic>().GetComponent<MeshRenderer>().material = currentSkin.skin;
            StartCoroutine(tempPlayer.GetComponent<PlayerWithPhysic>().changeScale());
            //tempPlayer.GetComponent<SkinLoader>().loadSkin(currentPlayerSkin);
        }
        else
        {
            tempPlayer = Instantiate(Player, new Vector3(mazeInstance.GetComponent<MazeSpawner>().CellWidth, 2, mazeInstance.GetComponent<MazeSpawner>().CellHeight), Quaternion.Euler(0, 0, 0)) as GameObject;
            tempPlayer.transform.parent = tempGround.transform;
            Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer);
            tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer = currentSkin;
            Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer + "  " + tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer.mass);
            tempPlayer.GetComponent<PlayerWithPhysic>().GetComponent<MeshRenderer>().material = currentSkin.skin;
            StartCoroutine(tempPlayer.GetComponent<PlayerWithPhysic>().changeScale());
            // tempPlayer.GetComponent<SkinLoader>().loadSkin(currentPlayerSkin);
        }

    }

    public void updateSkin(PlayerType newSkin)
    {
        Debug.Log("LOAD: new " + newSkin.type);
        // PlayerWithPhysic tempP = tempPlayer.GetComponent<PlayerWithPhysic>();
        currentSkin = newSkin;
        tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer = currentSkin;
        tempPlayer.GetComponent<PlayerWithPhysic>().GetComponent<MeshRenderer>().material = currentSkin.skin;
        StartCoroutine(tempPlayer.GetComponent<PlayerWithPhysic>().changeScale());
        // tempP.GetComponent<Rigidbody>().mass = tempP.typePlayer.mass;
        //tempPlayer.GetComponent<SkinLoader>().loadSkin(currentPlayerSkin);
    }
    public void resetGround()
    {

        if (tempPlayer.gameObject.activeSelf == false)
        {
            tempGround.GetComponent<RotationWorld>().enabled = false;
            //  Debug.Log("HERE");
        }
    }

    public void GameOver()
    {
        //if (tempPlayer.gameObject.GetComponent<PlayerWithPhysic>().playerDeath)
        playerDeath = true;

        if (playerLifes == 0)
        {
            Debug.Log("RETURN TO MENU GAME OVER");

            isGameOver = true;

        }
        else
        {
            playerLifes--;
        }



    }

    public void nextLevel()
    {
        endLevel = false;
        switch (currentLevel)
        {
            case 1:
                Debug.Log("RETURN TO MENU");

                currentLevel++;
                typeScene = SceneLevel.Level2;
                sceneType();
                restartGame();
                break;
            case 2:
                currentLevel++;
                typeScene = SceneLevel.Level3;
                sceneType();
                restartGame();
                break;
            case 3:
                Debug.Log("RETURN TO MENU");
                returnMenu();
                break;
        }

    }

    public void returnMenu()
    {
        playerDeath = false;


        Debug.Log("LOAD MENU SCENE");
        isLoaded = false;
        mainMenuLevel = true;
        isGameOver = false;
        MusicManager.Instance.StopAll();
        SFXManager.Instance.StopAll();
        typeScene = SceneLevel.MainMenu;
        // sceneType();
        SceneManager.LoadScene("MainMenu");
    }


    private void checkCurrentScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SampleScene") && !mainMenuLevel && !isLoaded)
        {

            Debug.Log("LEVEL LOAD");
            isLoaded = true;
            sceneType();
            Debug.Log("MUSIC NAME " + nameMusic);
            MusicManager.Instance.Play(nameMusic, 0, true);

            startGame();
        }
        else if (mainMenuLevel && !isLoaded)
        {
            Debug.Log("LOAD MENU");
            isLoaded = true;
            sceneType();
            MusicManager.Instance.Play(nameMusic, 0, true);

            startGame();
        }
    }
}
