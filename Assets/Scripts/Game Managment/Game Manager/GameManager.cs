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
    Credits,
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
    //Menu check
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
    // if the level restart 
    public bool restartLevel = false;
    // if the scene is loaded
    public bool isLoaded = false;
    // if the level is ended
    public bool endLevel = false;
    //if the player lost the game 
    public bool isGameOver = false;
    //is the game paused
    public bool isPaused = true;
    // is the player dead
    public bool playerDeath = false;
    public string nameMusic;
    [SerializeField] private GameObject canvas;
    //public GameObject crossFade;


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
        if (FindObjectsOfType(typeof(GameManager)).Length > 1)
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
        StartCoroutine(waitForTitle());
    }
    private IEnumerator waitForTitle()
    {
        Debug.Log("START ANIMATION");
        yield return new WaitForSeconds(6f);
        StartCoroutine(LoadGame.loadToMenuFormTitle(canvas));
    }
    public void sceneType()
    {
        switch (typeScene)
        {
            case SceneLevel.TitleCard:
                /// nameMusic = "MainMenuMusic";
                canvas = GameObject.Find("TitleCardCanvas");

                //crossFade = canvas.transform.Find("CrossFade").gameObject; ;
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

                //crossFade = canvas.transform.Find("CrossFade").gameObject;

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

               // crossFade = canvas.transform.Find("CrossFade").gameObject;
                currentLevel = 1;
                mazeRows = 4;
                mazeColums = 4;
                nameMusic = "Level1Music";
                heightPowerCamera = 100;
                break;
            case SceneLevel.Level2:
                canvas = GameObject.Find("LevelCanvas");

                //crossFade = canvas.transform.Find("CrossFade").gameObject;
                currentLevel = 2;
                mazeRows = 6;
                mazeColums = 6;
                nameMusic = "Level2Music";
                heightPowerCamera = 200;
                //loadLevel2
                break;
            case SceneLevel.Level3:
                canvas = GameObject.Find("LevelCanvas");

                //crossFade = canvas.transform.Find("CrossFade").gameObject;
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
        setUpMazeLevel();
 
    }

    /// ----------------------UPDATE CHECKING OF THE GAME MANAGER -------------------------///

    void Update()
    {
        checkCurrentScene();
        pausing();
     }
    public void callCheckCorutne()
    {
        StartCoroutine("checkStatusGame");
    }
     
    public IEnumerator checkStatusGame()
    {
        while (true)
        {
          //  checkCurrentScene();
          
            if (!mainMenuLevel)
            {

                if ((endLevel || playerDeath) && (Input.GetKeyDown(KeyCode.Space)))
                {

                    Debug.Log("SAVE");
                    endLevel = false;
                    if (!playerDeath)
                    {
                        SaveSystem.SaveScore(ScoringSystem.Instance);
                    }
                   // Debug.Log("LEVEL ENDED" + endLevel);
                    StartCoroutine(LoadGame.loadNextLevel(canvas));
                    // nextLevel();

                }
                else if (isGameOver && (Input.GetKeyDown(KeyCode.Space)))
                {
                    // Debug.Log("SAVE");
                    Debug.Log("END GAME IS HERE");
                    //LoadGame.load(crossFade.GetComponent<Animator>());
                    //returnMenu();
                    StartCoroutine(LoadGame.loadMainMenuFromGame(canvas));


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
           // Debug.Log("THIS IS LOOPING");
           // Debug.Log("LOOOOOOOOOOOOOOOOOOOOOOOOOOOP");
            if (Input.GetKeyUp(KeyCode.Space))
              {
                  Debug.Log("STOP");
                 StopCoroutine("checkStatusGame");
              }
            yield return null;

        }

    }
    //Check in wich Unity Scnen the game is corrently in and let load onec the level
    public void checkCurrentScene()
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
    private void pausing()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !mainMenuLevel && tempPlayer.activeSelf)
        {
            Debug.Log("PALYER IS ACTIVE: " + tempPlayer.activeSelf);
            if (Time.timeScale > 0 && tempPlayer != null)
            {
                Time.timeScale = 0;
                MusicManager.Instance.PauseMusic(nameMusic);
                SFXManager.Instance.stopSfxPlayer();
                isPaused = true;
                if (ScoreCurrentLevel.instance != null)
                {
                    ScoreCurrentLevel.instance.BoardMenu.SetActive(true);
                    ScoreCurrentLevel.instance.BoardMenu.transform.Find("PauseText").gameObject.SetActive(true);
                    if (ScoreCurrentLevel.instance.needToComplete.gameObject.activeSelf)
                    {
                        StopCoroutine(ScoreCurrentLevel.instance.notLevelComplete());
                        ScoreCurrentLevel.instance.needToComplete.gameObject.SetActive(false);
                    }

                        
                }
            }
            //Debug.Log("PAUSE");

        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && isPaused)
        {
            Time.timeScale = 1;
            // Debug.Log("RESUME");
            if (tempPlayer != null && tempPlayer.GetComponent<Rigidbody>().velocity.magnitude > 0)
                SFXManager.Instance.PlaySoundPlayer("RollingBallMainLoop", 0, true);
            if (ScoreCurrentLevel.instance != null)
            {
                ScoreCurrentLevel.instance.BoardMenu.SetActive(false);
                ScoreCurrentLevel.instance.BoardMenu.transform.Find("PauseText").gameObject.SetActive(false);
            }
            
            MusicManager.Instance.ResumeMusic(nameMusic, true);
            isPaused = false;
        }

    }

    // We update the skin of the player form the savefile
    public void updateSkin(PlayerType newSkin)
    {
        Debug.Log("LOAD: new " + newSkin.type);
        // PlayerWithPhysic tempP = tempPlayer.GetComponent<PlayerWithPhysic>();
        currentSkin = newSkin;
        tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer = currentSkin;
        tempPlayer.GetComponent<PlayerWithPhysic>().GetComponent<MeshRenderer>().material = currentSkin.skin;
        StartCoroutine(tempPlayer.GetComponent<PlayerWithPhysic>().changeScale());

    }
    /// ---------------------------------------------------------------///
    

    ///-------------------------BUILDING THE MAZE LEVEL-------------------------------------///
    public void setUpMazeLevel()// here we build the maze in base of the level (to expand)
    {
        buildmaze();
        addPlayer();
    }
    private void buildmaze()
    {
        mazeInstance = Instantiate(maze) as MazeSpawner;
        tempGround = Instantiate(Ground, new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, 0, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2), Quaternion.Euler(0, 0, 0)) as GameObject;
        tempGround.GetComponent<RotationWorld>().enabled = false;
        StartCoroutine(LoadGame.WaitForFade(canvas));
        mazeInstance.transform.parent = tempGround.transform;
        tmpCamera = Instantiate(playerCamera, playerCamera.transform.position, playerCamera.transform.rotation) as Camera;
        tmpCamera.transform.position = new Vector3(transform.position.x, playerCamera.gameObject.GetComponent<FollowPlayer>().height, transform.position.z);
        if (!mainMenuLevel)
        {
            tmpLevelManager = Instantiate(LevelManagement, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

        }
        if (tempCameraPower == null)
        {
            tempCameraPower = Instantiate(powerUpCamera, new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, heightPowerCamera, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2), Quaternion.Euler(90, 0, 0)) as GameObject;
            tempCameraPower.GetComponent<Camera>().enabled = false;
        }
        else tempCameraPower.transform.position = new Vector3((mazeRows * mazeInstance.GetComponent<MazeSpawner>().CellWidth) / 2, heightPowerCamera, (mazeColums * mazeInstance.GetComponent<MazeSpawner>().CellHeight) / 2);
        
    }
    public void addPlayer()
    {
       // Debug.Log("ADD PLAYER");
        if (!mainMenuLevel)
        {
            tempPlayer = Instantiate(Player, new Vector3(mazeInstance.transform.position.x, 2, mazeInstance.transform.position.z), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            tempPlayer.transform.parent = tempGround.transform;
            //Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer);
            tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer = currentSkin;
           // Debug.Log(tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer + "  " + tempPlayer.GetComponent<PlayerWithPhysic>().typePlayer.mass);
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
 
    /// ---------------------------------------------------------------///

    ///--------------- RESTART GAME FUNCTIONALITY --------------------------------///
    //restart the game scene if either the player is dead or the level is complete
    private void restartGame()
    {
        Debug.Log("Restart");
        if (!playerDeath)
         MusicManager.Instance.StopAll();
        Destroy(tmpCamera.gameObject);
        Destroy(tmpLevelManager);
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
    /// chage the current menu scene type
    public IEnumerator changeMenuScene()
    {
        tempPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        StartCoroutine(LoadGame.loadNewMenu(canvas));
        yield return new WaitForSeconds(2f);
        Destroy(mazeInstance.gameObject);
        Destroy(tempGround);
        Destroy(tempPlayer);
        Destroy(tmpCamera.gameObject);
        Destroy(tmpLevelManager);



        
        setUpMazeLevel();

    }
    //This is the courtine that fade in and out the music volume level
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        MusicManager.Instance.Play(nameMusic, 0, true);
        restartLevel = false;

    }
    /// ---------------------------------------------------------------///





    /// -------------------------GAME OVER FUNCTIONALITY--------------------------------///
    public void GameOver()
    {
        //if (tempPlayer.gameObject.GetComponent<PlayerWithPhysic>().playerDeath)

       
        if (playerLifes <= 0)
        {
            Debug.Log("RETURN TO MENU GAME OVER");

            isGameOver = true;
           
        }
        else
        {
            playerDeath = true;
            playerLifes--;
            restartLevel = true;
          
        }



    }
    
    public void resetGround()
    {

        if (tempPlayer.gameObject.activeSelf == false)
            tempGround.GetComponent<RotationWorld>().enabled = false;
   
    }
    /// ---------------------------------------------------------------///
    ///LEVEL SELECTION IF PALYER LOST LIFE OR COMPLETED THE LEVEL///
    public void nextLevel()
    {
        endLevel = false;
        if (playerLifes >= 0)
        {
            switch (currentLevel)
            {
                case 1:
                    if (!playerDeath)
                    {
                        currentLevel++;
                        typeScene = SceneLevel.Level2;
                    }
                    else typeScene = SceneLevel.Level1;
                    sceneType();
                    restartGame();
                    break;
                case 2:
                    if (!playerDeath)
                    {
                        currentLevel++;
                        typeScene = SceneLevel.Level3;
                    }
                    else typeScene = SceneLevel.Level2;
                    sceneType();
                    restartGame();
                    break;
                case 3:
                    if (playerDeath)
                    {
                        Debug.Log("PLAYER IS DEAD");
                        typeScene = SceneLevel.Level3;
                        sceneType();
                        restartGame();
                    }
                    else
                    {
                        Debug.Log("Level ENDED");
                        returnMenu(); 
                    }
                   
                    
                    break;
            }
        }

    }
    private int levelSelection(int counter)
    {
        if (playerDeath)
            return counter;
        else
            return counter++;
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
        playerLifes = 1;
        SceneManager.LoadScene("MainMenu");
        //StartCoroutine(canvas);
    }
    /// ---------------------------------------------------------------///


}
