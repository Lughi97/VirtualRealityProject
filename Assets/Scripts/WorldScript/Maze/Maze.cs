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
       
        while (activeCells.Count > 0)
        {
            Debug.Log("Before " + activeCells.Count);
            nextGenerationStep(activeCells);
            Debug.Log("After " + activeCells.Count);
        }
       
    }
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
          firstGenerationStep(activeCells);
        //  while (activeCells.Count > 0)
        //  {
        //     yield return delay;
        //    nextGenerationStep(activeCells);
        //}

        for (int x = 0; x < size.x; x++)
            for (int z = 0; z < size.z; z++)
            {
                //activeCells.Add(createCell(new IntVector2(x, z)));
                nextGenerationStep(activeCells);
                yield return delay;

                



            }
       
    }
    /*  
   public IEnumerator generate()
   {
       WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
       cells = new MazeCell[size.x, size.z];
       IntVector2 coordinates = RandomCoordinates;
       List<MazeCell> activeCells = new List<MazeCell>();
       //generationFirstStep(activeCells);
       activeCells.Add(createCell(RandomCoordinates));
       Debug.Log(activeCells);
       //while (ContainsCoordinates(coordinates) && getCell(coordinates) == null
       while (activeCells.Count>0)
       {
           yield return delay;
           //createCell(coordinates);
           //coordinates += MazeDirections.RandomValue.toIntVector2();
           // generationNextStep(activeCells);
           int currentIndex = activeCells.Count - 1;
           MazeCell currentCell = activeCells[currentIndex];
           MazeDirection direction = MazeDirections.RandomValue;
           //Debug.Log(currentCell);
           IntVector2 coordinatesfinal = RandomCoordinates;
           if (ContainsCoordinates(coordinatesfinal) && getCell(coordinatesfinal) == null)
           {
               activeCells.Add(createCell(coordinatesfinal));
           }
           else
           {
               activeCells.RemoveAt(currentIndex);
           }
       }

       /* switch to select between regulare and random maze.
       /*for (int x = 0; x < size.x; x++)
           for (int z = 0; z < size.z; z++)
           {
               yield return delay;
               createCell(new IntVector2(x,z));
           }

   }
   */
    /*get random coordinates*/
    public IntVector2 RandomCoordinates
    {
       get{
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void firstGenerationStep(List<MazeCell> active)
    {
        IntVector2 coord = new IntVector2(0, 0);
        active.Add(createCell(RandomCoordinates));
    }

    private void nextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        Debug.Log(direction);
        IntVector2 coordinates = currentCell.coordinates + direction.toIntVector2();
        if (ContainsCoordinates(coordinates) && getCell(coordinates) == null)
        {
            activeCells.Add(createCell(coordinates));
        }
        else
        {

            activeCells.RemoveAt(currentIndex);
        }
    }
        /*
        private void generationFirstStep(List<MazeCell> active)
        {
            IntVector2 cord = RandomCoordinates;

            active.Add(createCell(RandomCoordinates));
            Debug.Log(createCell(RandomCoordinates));

        }

        private void generationNextStep(List<MazeCell> active)
        {
            int currentIndex = active.Count - 1;
            MazeCell currentCell = active[currentIndex];
            MazeDirection direction = MazeDirections.RandomValue;
            IntVector2 coordinates = currentCell.localCoordinates + direction.toIntVector2();
            if (ContainsCoordinates(coordinates) && getCell(coordinates) == null)
            {
                active.Add(createCell(coordinates));
            }
            else
            {
                active.RemoveAt(currentIndex);
            }
        }
        */
        /*create the cells*/
     private MazeCell createCell(IntVector2 coordinates)
     {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        
        //Debug.Log(x * newCell.transform.localScale.x / 2 - sizeX * newCell.transform.localScale.x / 2);
        newCell.transform.localPosition = new Vector3(coordinates.x* newCell.transform.localScale.x - size.x *newCell.transform.localScale.x  + newCell.transform.localScale.x, 0f, coordinates.z * newCell.transform.localScale.z - size.z * 0);
        return newCell;
     }
}
