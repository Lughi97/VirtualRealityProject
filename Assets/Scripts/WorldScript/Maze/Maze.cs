using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    //cell size
    //public int sizeX, sizeZ;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    public IntVector2 size;
    private MazeCell[,] cells;


  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MazeCell getCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }


    public void generateGround()
    {
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        firstGenerationStep(activeCells);
        int retreis = 1;
    

        while (activeCells.Count > 0)
        {
           // Debug.Log("Cell Active " + activeCells.Count);
            nextGenerationStep(activeCells,retreis);
          
           
        }
       
    }

    public IntVector2 RandomCoordinates{
        get {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
          firstGenerationStep(activeCells);

        while (activeCells.Count > 0)
        {
            yield return delay;
            //nextGenerationStep(activeCells,retries);
        }
    
    }

  
    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
    // the first block place in the ground
    private void firstGenerationStep(List<MazeCell> active)
    {
        IntVector2 coord = new IntVector2(0, 0);
        active.Add(createCell(RandomCoordinates));
    }
    // all the next block to create the maze with backtracking
    private void nextGenerationStep(List<MazeCell> activeCells, int retries)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        do
        {
            MazeDirection direction = MazeDirections.RandomValue;
            //Debug.Log(direction);
            IntVector2 coordinates = currentCell.coordinates + direction.toIntVector2();

            Debug.Log(retries);
            if (ContainsCoordinates(coordinates) && getCell(coordinates) == null)
            {
                //Debug.Log(coordinates.x +"&& " +coordinates.z); 
                activeCells.Add(createCell(coordinates));
                break;
            }
            else
            {
                retries--;
                if (retries == 0) activeCells.RemoveAt(currentIndex);
            }
        }
        while (retries >= 0);
       
    }
  
      
        /*create the cells*/
     private MazeCell createCell(IntVector2 newcoords)
     {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[newcoords.x, newcoords.z] = newCell;
        newCell.coordinates = newcoords;
        newCell.name = "Maze Cell " + newcoords.x + ", " + newcoords.z;
        newCell.transform.parent = transform;
        
        //Debug.Log(x * newCell.transform.localScale.x / 2 - sizeX * newCell.transform.localScale.x / 2);
        newCell.transform.localPosition = new Vector3(newcoords.x* newCell.transform.localScale.x - size.x *newCell.transform.localScale.x  + newCell.transform.localScale.x, 0f, newcoords.z * newCell.transform.localScale.z - size.z * 0);
        return newCell;
     }
}
