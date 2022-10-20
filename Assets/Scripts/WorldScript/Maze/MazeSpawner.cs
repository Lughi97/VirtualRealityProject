using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// This is the spawner of the maze here we the agorithm calls the maze generator to create the maze with all the cells
/// Here we instantiate the floors, walls and all the object needed in the scene
/// </summary>
public class MazeSpawner : PlaceInMaze
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        MenuMaze
    }
    [Header("Maze Genarator")]
    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public int Rows;
    public int Columns;
    public float CellWidth;
    public float CellHeight;
    public bool AddGaps = false;

    [Header("LevelObjects")]
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public GameObject BlackHoles = null;
    public GameObject[] collectable = null;
    // public GameObject[] creates = null;
    public GameObject CameraPowerUp;
    public GameObject GoalPrefab = null;
    private MazeGenerator mMazeGenerator = null;

    [Header("MainMenu")]
    //Menu
    [SerializeField]
    private List<GameObject> menuText;
    [SerializeField]
    private GameObject menuReturn;


    [Header("HighScoresMenu")]
    [SerializeField]
    private List<GameObject> LevelText;
    [SerializeField]
    private GameObject singleScore;


    [Header("Shop Menu")]
    [SerializeField]
    private GameObject playerSphereType;
  //  [SerializeField]
   // private List<PlayerPrefs> skins;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.mainMenuLevel)
            Algorithm = MazeGenerationAlgorithm.MenuMaze;
        else Algorithm = MazeGenerationAlgorithm.PureRecursive;


        Rows = GameManager.Instance.mazeRows;
        Columns = GameManager.Instance.mazeColums;

        if (!FullRandom)
        {
            Random.InitState(RandomSeed);
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeAlgorithm(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.MenuMaze:
                mMazeGenerator = new MenuMaze(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        if (Algorithm != MazeGenerationAlgorithm.MenuMaze)
            CreateLevelWorld();
        else
            CreateMainMenu();


    }
    private void CreateLevelWorld()
    {
        PlaceInMaze.getValueMaze(CellWidth, CellHeight,Rows,Columns);
        for (int row = 0; row < Rows; row++)
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;


                if (row != 0 && row != Rows && column != 0 && column != Columns)
                {
                    //placeBlackHoles(x,z,tmp)
                    PlaceInMaze.placeBlackHoles(BlackHoles,x, z, tmp);
                    //placeCollectables(x, z, tmp);
                    PlaceInMaze.placeCollectables(collectable, x, z, tmp);
                }
                PlaceInMaze.placeWalls(transform,Wall,x, z, cell);
            }

        PlaceInMaze.placePillars(transform,Pillar);
        if (GoalPrefab != null)
        {
            // Debug.Log("HEELO");
            GameObject tmp;
            float x = Columns * (CellWidth + (AddGaps ? .2f : 0));
            float z = Rows * (CellHeight + (AddGaps ? .2f : 0));
            int RandNumber = Random.Range(1, 3);
            switch (RandNumber)
            {
                // case 1:
                //     tmp = Instantiate(GoalPrefab, new Vector3(x - CellWidth, 0, 0), GoalPrefab.transform.rotation) as GameObject;
                //      tmp.transform.parent = transform;
                //      break;
                case 1:
                    tmp = Instantiate(GoalPrefab, new Vector3(0, 0, z - CellHeight), GoalPrefab.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                    break;
                case 2:
                    tmp = Instantiate(GoalPrefab, new Vector3(x - CellWidth, 0, z - CellHeight), GoalPrefab.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                    break;
            }



        }

    }
    private void CreateMainMenu()
    {
        float X = Columns * (CellWidth + (AddGaps ? .2f : 0));
        float Z = Rows * (CellHeight + (AddGaps ? .2f : 0));
        Vector3 position = new Vector3(X - CellWidth, 0, 0);
        PlaceInMaze.getValueMaze(CellWidth, CellHeight, Rows, Columns);
        for (int row = 0; row < Rows; row++)
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                PlaceInMaze.placeWalls(transform, Wall, x, z, cell);

            }
        PlaceInMaze.placePillars(transform, Pillar);
        switch (GameManager.Instance.typeScene)
        {
            case SceneLevel.MainMenu:
                PlaceInMaze.placeMenuComponents(transform, menuText);
                break;
            case SceneLevel.HighScoreMenu:
                PlaceInMaze.placeHighScores(transform, LevelText, singleScore);
                PlaceInMaze.placeReturnMenu(transform, menuReturn, position);
                break;
            case SceneLevel.Settings:
                Debug.Log("ADD SETTINGS");
                PlaceInMaze.placeReturnMenu(transform, menuReturn, position);
                break;
            case SceneLevel.Shop:
                Debug.Log("ADD SHOP");
               
                PlaceInMaze.placeSkinPlayer(transform,GameManager.Instance.skinsPlayer, playerSphereType);
                PlaceInMaze.placeReturnMenu(transform, menuReturn, position);
                break;
        }
    }
}

