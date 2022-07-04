using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    //cell 
    public MazeCell cellPrefab;
    public float generationStepDelay;
    public IntVector2 size;
    private MazeCell[,] cells;

    public MazePassage passagePrefab;
    public MazeWall wallPrefab;

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
           // Debug.Log("Cell Active " + activeCells.Count);
            nextGenerationStep(activeCells);
        }
       
    }

    public IntVector2 randomCoordinates{
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
            nextGenerationStep(activeCells);
        }
    
    }

  
    public bool containsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
    // the first block place in the ground
    private void firstGenerationStep(List<MazeCell> active)
    {
        IntVector2 coord = new IntVector2(0, 0);
        active.Add(createCell(randomCoordinates));
    }
    // all the next block to create the maze with walls and passages
    private void nextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.isFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.randomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.toIntVector2();
        if (containsCoordinates(coordinates))
        {
            MazeCell neighbor = getCell(coordinates);
            if (neighbor == null)
            {
                neighbor = createCell(coordinates);
                createPassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                createWall(currentCell, neighbor, direction);
                // No longer remove the cell here.
            }
        }
        else
        {
            createWall(currentCell, null, direction);
            
            // No longer remove the cell here.
        }
    }
	
    private void createPassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {//give udirectional edge current<->next
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.initialize(otherCell, cell, direction.getOpposite());
    }

    private void createWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {//give udirectional edge current<->next
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.initialize(otherCell, cell, direction.getOpposite());
        }
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
