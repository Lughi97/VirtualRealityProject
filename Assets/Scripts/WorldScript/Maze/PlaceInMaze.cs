using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public  class  PlaceInMaze:MonoBehaviour
{
    public static float cellWidth;
    public static float cellHeight;
    public static int currentRows;
    public static int currentColumns;
  
  
    protected static void getValueMaze(float width, float height, int rows, int columns)
    {
       // Debug.Log("USED GET VALUE MAZE STATIC FUNCTION");
        cellWidth = width;
        cellHeight = height;
        currentRows = rows;
        currentColumns = columns;
    }

    protected static void placeWalls(Transform parent,GameObject wall,float x, float z, MazeCell cell)
    {
        //Debug.Log("USED PLACE WALL STATIC FUNCTION");
        GameObject tmpWall;
        if (cell.WallRight)
        {
            tmpWall = Instantiate(wall, new Vector3(x + cellWidth / 2, wall.transform.localScale.y / 2, z) + wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
            tmpWall.transform.parent = parent;
        }
        if (cell.WallFront)
        {
            tmpWall = Instantiate(wall, new Vector3(x, wall.transform.localScale.y / 2, z + cellHeight / 2) + wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
            tmpWall.transform.parent = parent;
        }
        if (cell.WallLeft)
        {
            tmpWall = Instantiate(wall, new Vector3(x - cellWidth / 2, wall.transform.localScale.y / 2, z) + wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
            tmpWall.transform.parent = parent;
        }
        if (cell.WallBack)
        {
            tmpWall = Instantiate(wall, new Vector3(x, wall.transform.localScale.y / 2, z - cellHeight / 2) + wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
            tmpWall.transform.parent = parent;
        }
    }
    protected static void placePillars(Transform parent, GameObject pillar)
    {
       // Debug.Log("USED PLACE PILLARS STATIC FUNCTION");
        if (pillar != null)
        {
            for (int row = 0; row <= currentRows; row++)
                for (int column = 0; column <= currentColumns; column++)
                {

                    float x = column * (cellWidth);// + (AddGaps ? .2f : 0));
                    float z = row * (cellHeight);// + (AddGaps ? .2f : 0));
                    GameObject tmp;
                    Vector3 positionPillar = new Vector3(x - cellWidth / 2, pillar.transform.localPosition.y, z - cellHeight / 2);
                    if (GameManager.Instance.mainMenuLevel)
                    {
                        if (((column == 0 || column == currentColumns) && row <= currentRows) || (((row == 0 || row == currentRows) && column <= currentColumns)))
                        {
                            tmp = Instantiate(pillar, positionPillar, pillar.transform.rotation) as GameObject;
                            tmp.transform.parent = parent;
                        }
                    }
                    else
                    {
                        tmp = Instantiate(pillar, positionPillar, pillar.transform.rotation) as GameObject;
                        tmp.transform.parent = parent;
                    }


                }
        }
    }
    //add black holes to the scene that kills the player
    protected static void placeBlackHoles(GameObject hole,float x, float z, GameObject floorTmp)
    {
       // Debug.Log("USED PLACE BLACK HOLES FUNCTION");
        int side = Random.Range(0, 4);
        int random = Random.Range(1, 100);
        GameObject tmp;
        if (hole != null)
        {

            if (random % 6 == 0)
            {
                switch (side)
                {
                    case 0:
                        tmp = Instantiate(hole, new Vector3(floorTmp.transform.position.x + floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z + floorTmp.transform.localScale.x / 3), hole.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 1:
                        tmp = Instantiate(hole, new Vector3(floorTmp.transform.position.x - floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z + floorTmp.transform.localScale.x / 3), hole.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 2:
                        tmp = Instantiate(hole, new Vector3(floorTmp.transform.position.x - floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z - floorTmp.transform.localScale.x / 3), hole.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 3:
                        tmp = Instantiate(hole, new Vector3(floorTmp.transform.position.x + floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z - floorTmp.transform.localScale.x / 3), hole.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 4:
                        tmp = Instantiate(hole, new Vector3(floorTmp.transform.position.x, 0f, floorTmp.transform.position.z), hole.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;

                }


            }
        }
    }

    protected static void placeCollectables(GameObject[] collectables,float x, float z, GameObject floorTmp)
    {
      
        // Debug.Log("USED PLACE COLLECTABLES STATIC FUNCTION");
        int chance = Random.Range(1, 80);
        int randomCollectable = Random.Range(0, 5);
        if (collectables != null && x != 0 && z != 0)
        {

            GameObject tmpCoin;
            GameObject tmpCreate;
            GameObject tmpPower;
            // GameObject tmpPoerArrow;
            switch (randomCollectable)
            {
                case 0:
                   
                        tmpCoin = Instantiate(collectables[0], new Vector3(floorTmp.transform.position.x, 2f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmpCoin.transform.parent = floorTmp.transform;
                   
                    break;
                case 1:
                    if (chance % 2 == 0)
                    {
                        tmpCoin = Instantiate(collectables[1], new Vector3(floorTmp.transform.position.x, 2f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmpCoin.transform.parent = floorTmp.transform;
                    }
                    break;
                case 2:
                    if (chance % 3 == 0)
                    {
                        tmpCoin = Instantiate(collectables[2], new Vector3(floorTmp.transform.position.x, 2f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmpCoin.transform.parent = floorTmp.transform;
                    }
                    break;
                case 3:
                    if (chance % 5 == 0)
                    {
                        tmpCreate = Instantiate(collectables[3], new Vector3(floorTmp.transform.position.x, 3f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmpCreate.transform.parent = floorTmp.transform;
                    }
                    break;
                case 4:
                    if (chance % 3 == 0)
                    {
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                // Debug.Log("HELLO");
                                tmpPower = Instantiate(collectables[4], new Vector3(floorTmp.transform.position.x, 3f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                                tmpPower.transform.parent = floorTmp.transform;
                                break;
                            case 1:
                                // Debug.Log("HEYYYYYYYY");
                                tmpPower = Instantiate(collectables[5], new Vector3(floorTmp.transform.position.x, 3f, floorTmp.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                                tmpPower.transform.parent = floorTmp.transform;
                                break;

                            case 2:
                                // Debug.Log("HEYYYYYYYY");
                                int[] angles = { 0, 90, 180, -90 };
                                int currentAngle = angles[Random.Range(0, angles.Length)];



                                tmpPower = Instantiate(collectables[6], new Vector3(floorTmp.transform.position.x, 1f, floorTmp.transform.position.z), Quaternion.Euler(0, currentAngle, 0)) as GameObject;
                                tmpPower.transform.parent = floorTmp.transform;
                                if (currentAngle == 0) tmpPower.transform.position = new Vector3(floorTmp.transform.position.x - 7, 1f, floorTmp.transform.position.z);
                                else if (currentAngle == 90) tmpPower.transform.position = new Vector3(floorTmp.transform.position.x, 1f, floorTmp.transform.position.z + 7);
                                else if (currentAngle == 180) tmpPower.transform.position = new Vector3(floorTmp.transform.position.x + 7, 1f, floorTmp.transform.position.z);
                                else if (currentAngle == -90) tmpPower.transform.position = new Vector3(floorTmp.transform.position.x, 1f, floorTmp.transform.position.z - 7);
                                break;

                        }
                    }
                    break;


            }



        }
    }

    protected static void placeMenuComponents(Transform parent,List<GameObject> menuText)
    {
        //Debug.Log("HEELO");
        //Debug.Log("USED PLACE MENU COMPONETNS STATIC FUNCTION");
        float x = currentColumns * (cellWidth);// + (AddGaps ? .2f : 0));
        float z = currentColumns * (cellHeight);//+ (AddGaps ? .2f : 0));
        foreach (GameObject element in menuText)
        {
            if (element != null)
            {
                switch (element.name)
                {


                    case "StartGame":
                        istantiateObjMenuType(parent,element, new Vector3(0, 0, z - cellHeight));
                        break;
                    case "Credits":
                        istantiateObjMenuType(parent,element, new Vector3(x - cellWidth, 0, z - cellHeight));
                        break;
                    case "Shop":
                        istantiateObjMenuType(parent, element, new Vector3(x - cellWidth, 0, z - (2*cellHeight)));
                        break;
                    case "LevelScore":
                        istantiateObjMenuType(parent,element, new Vector3(x - cellWidth, 0, 0));
                        break;

                    case "Quit":
                        istantiateObjMenuType(parent,element, Vector3.zero);
                        break;


                }

            }

        }
    }
    protected static void placeCredits(Transform parent, List<GameObject> creditsText)
    {
        float x = currentColumns * (cellWidth);// + (AddGaps ? .2f : 0));
        float z = currentRows * (cellHeight);// + (AddGaps ? .2f : 0));
        foreach (GameObject element in creditsText)
        {
            if (element != null)
            {
                switch (element.name)
                {
                    case "ArtCredits":
                        istantiateObjMenuType(parent, element, new Vector3((x - cellWidth)/3, 0.5f, (z - cellHeight)/2));
                        break;
                    case "Developers":
                        istantiateObjMenuType(parent, element, new Vector3(((x - cellWidth) * 110) / 100, 0.5f, ((z - cellHeight) * 70) / 100));
                        break;
                    case "UnimiCredits":
                        istantiateObjMenuType(parent, element, new Vector3((x - cellWidth)/2, 0.5f, ((z - cellHeight))));
                        break;
                }
            }
        }

    }
    protected static void placeHighScores(Transform parent,List<GameObject> levelText, GameObject selctedLevelText)
    {
        //Debug.Log("USED PLACE HIGH SCORES STATIC FUNCTION");
        float x = currentColumns * (cellWidth);// + (AddGaps ? .2f : 0));
        float z = currentRows * (cellHeight);// + (AddGaps ? .2f : 0));
        foreach (GameObject element in levelText)
        {
            if (element != null)
            {
                switch (element.name)
                {
                    case "Level1Score":
                        istantiateObjMenuType(parent,element, new Vector3(0, 0.5f, z - cellHeight));
                        CreateList(parent,element, selctedLevelText, new Vector3(0, 0.5f, z - cellHeight));
                        break;

                    case "Level2Score":
                        istantiateObjMenuType(parent,element, new Vector3((x - cellWidth) / 2, 0.5f, z - cellHeight));
                        CreateList(parent,element, selctedLevelText, new Vector3((x - cellWidth) / 2, 0.5f, z - cellHeight));
                        break;

                    case "Level3Score":
                        istantiateObjMenuType(parent,element, new Vector3(x - cellWidth, 0.5f, z - cellHeight));
                        CreateList(parent,element, selctedLevelText, new Vector3(x - cellWidth, 0.5f, z - cellHeight));
                        break;
                }
            }
        }
    }
    protected static void placeSkinPlayer(Transform parent,List<PlayerType> skins, GameObject sphere)
    {
        float x = currentColumns * (cellWidth);// + (AddGaps ? .2f : 0));
        float z = currentRows * (cellHeight);// + (AddGaps ? .2f : 0));

        foreach ( PlayerType element in skins)
        {
           
            if (element != null)
            {
               // Debug.Log(element.type);
                switch (element.type)
                {
                    

                    case typeBall.wood:
                        istantiateObjMenuType(parent, sphere, new Vector3(0, 2.5f, 0));
                        sphere.GetComponent<MeshRenderer>().material = element.skin;
                        sphere.GetComponent<ChangeSkin>().skinToEquip = element;
                        break;
                    case typeBall.metal1:
                        istantiateObjMenuType(parent, sphere, new Vector3(0, 2f, z-cellHeight));
                        sphere.GetComponent<MeshRenderer>().material = element.skin;
                        sphere.GetComponent<ChangeSkin>().skinToEquip = element;
                        break;
                    case typeBall.football:
                        istantiateObjMenuType(parent, sphere, new Vector3(x-cellWidth, 2f, z-cellHeight));
                        sphere.GetComponent<MeshRenderer>().material = element.skin;
                        sphere.GetComponent<ChangeSkin>().skinToEquip = element;
                        break;
                    case typeBall.basketball:
                        istantiateObjMenuType(parent, sphere, new Vector3((x-cellWidth)/2, 2f, (z - cellHeight)));
                        sphere.GetComponent<MeshRenderer>().material = element.skin;
                        sphere.GetComponent<ChangeSkin>().skinToEquip = element;
                        break;
                    case typeBall.tennis:
                        istantiateObjMenuType(parent, sphere, new Vector3((x - cellWidth), 2f, (z - cellHeight)/2));
                        sphere.GetComponent<MeshRenderer>().material = element.skin;
                        sphere.GetComponent<ChangeSkin>().skinToEquip = element;
                        break;
                }
            }
        }
    }

    private static void CreateList(Transform parent,GameObject levelScore,GameObject scoreSelected ,Vector3 position)
    {
        int remove = 5;
        List<int> tempList = new List<int>();
        if (levelScore.name.Contains("1"))
            tempList = ScoringSystem.Instance.HighScoreLevel1;
        if (levelScore.name.Contains("2"))
            tempList = ScoringSystem.Instance.HighScoreLevel2;
        if (levelScore.name.Contains("3"))
            tempList = ScoringSystem.Instance.HighScoreLevel3;
        foreach (int element in tempList)
        {

            GameObject tmp;
            Vector3 newPosition = position - new Vector3(-5, 0, remove);
            tmp = Instantiate(scoreSelected, newPosition, scoreSelected.transform.rotation);
            tmp.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + element;
            remove += 5;
            tmp.transform.parent = parent;

        }

    }
    private static void istantiateObjMenuType(Transform parent,GameObject obj, Vector3 position)
    {
        GameObject tmp = Instantiate(obj, position, obj.transform.localRotation) as GameObject;
        tmp.transform.parent = parent;
    }

    protected static void placeReturnMenu(Transform parent, GameObject menuReturn, Vector3 position)
    {
      
        GameObject tmp = Instantiate(menuReturn, position, menuReturn.transform.localRotation) as GameObject;
        tmp.transform.parent = parent;
    }
}

