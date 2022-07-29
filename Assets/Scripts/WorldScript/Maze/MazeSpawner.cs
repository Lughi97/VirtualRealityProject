using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive
    }
    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public GameObject BlackHoles = null;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = false;
    public GameObject GoalPrefab = null;
    private MazeGenerator mMazeGenerator = null;
    // Start is called before the first frame update
    void Start()
    {
        if (!FullRandom)
        {
            Random.InitState(RandomSeed);
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeAlgorithm(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(90, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                placeBlackHoles(x,z,tmp);
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2,Wall.transform.localScale.y / 2, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, Wall.transform.localScale.y/2, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2,  Wall.transform.localScale.y / 2, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x,Wall.transform.localScale.y / 2, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                }
               // if (GoalPrefab != null && row==Rows && column==Columns)
            //    {
             //       tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
             //       tmp.transform.parent = transform;
             //   }
            }
        if (Pillar != null)
        {
            for(int row=0; row<=Rows;row++)
                for (int column = 0; column <= Columns; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp;
                    tmp = Instantiate(Pillar, new Vector3(x-CellWidth/2,Pillar.transform.localScale.y, z-CellHeight/2), Pillar.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                }
        }
        if (GoalPrefab != null)
        {
            GameObject tmp;
            float x = Columns * (CellWidth + (AddGaps ? .2f : 0));
            float z = Rows * (CellHeight + (AddGaps ? .2f : 0));
            int RandNumber = Random.Range(1, 4);
            switch (RandNumber)
            {
                case 1:
                    tmp = Instantiate(GoalPrefab, new Vector3(x-CellWidth, 0,0), GoalPrefab.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                    break;
                case 2:
                    tmp = Instantiate(GoalPrefab, new Vector3(0, 0, z-CellHeight), GoalPrefab.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                    break;
                case 3:
                    tmp = Instantiate(GoalPrefab, new Vector3(x - CellWidth, 0, z - CellHeight), GoalPrefab.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                    break;
            }
          


        }
    }
    //add black holes to the scene that kills the player
    public void placeBlackHoles(float x, float z,GameObject floorTmp)
    {
        int side = Random.Range(0,4);
        int random=Random.Range(1, 100);
        GameObject tmp;
        if (BlackHoles != null && x!=0 && z!=0)
        {
           
            if (random % 5 == 0)
            {
                 switch (side)
                  {
                    case 0:
                        tmp = Instantiate(BlackHoles, new Vector3(floorTmp.transform.position.x + floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z + floorTmp.transform.localScale.x / 3), BlackHoles.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 1:
                        tmp = Instantiate(BlackHoles, new Vector3(floorTmp.transform.position.x - floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z + floorTmp.transform.localScale.x / 3), BlackHoles.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                   case 2:
                        tmp = Instantiate(BlackHoles, new Vector3(floorTmp.transform.position.x - floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z - floorTmp.transform.localScale.x / 3), BlackHoles.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                     case 3:
                        tmp = Instantiate(BlackHoles, new Vector3(floorTmp.transform.position.x + floorTmp.transform.localScale.x / 3, 0f, floorTmp.transform.position.z - floorTmp.transform.localScale.x / 3), BlackHoles.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;
                    case 4:
                        tmp = Instantiate(BlackHoles, new Vector3(floorTmp.transform.position.x, 0f, floorTmp.transform.position.z ), BlackHoles.transform.rotation) as GameObject;
                        tmp.transform.parent = floorTmp.transform;
                        break;

                }


            }
        }
    }
}
