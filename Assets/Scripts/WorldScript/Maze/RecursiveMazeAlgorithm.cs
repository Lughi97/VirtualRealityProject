using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Recursive Algorithm to create the maze 
///  1) start form the corner with 2 walls 2 open spaces
///  2) check if the cell was visited
///  3)Is visited is false in the edges (there are no cells)
///  4)Corners has 2 open direction
///  5)Randomly pick those directions to dertemine where to move next by NSEW
///  6) put the wall in the open direction that we dind't visit
///  7)We move to the next cell (the recursion) and rpeat
/// </summary>
public class RecursiveMazeAlgorithm : MazeGenerator
{
    public RecursiveMazeAlgorithm(int rows, int columns) : base(rows, columns)
    {
    }
    // build the maze
    public override void GenerateMaze()
    {
        VisitCell(0, 0, Direction.Start);
    }
    private void VisitCell(int row, int column, Direction moveMade)
    {
        Direction[] movesAvailable = new Direction[4];// the NSWE directions
        int movesAvailableCount = 0;// number of moves that can be done 
        do
        {
            movesAvailableCount = 0;
            //check move right place wall left
           
            if (column + 1 < ColumnCount && !GetMazeCell(row, column + 1).IsVisited)
            {
               // Debug.Log(movesAvailableCount);
                movesAvailable[movesAvailableCount] = Direction.Right;
                movesAvailableCount++;
            }
            else if (!GetMazeCell(row, column).IsVisited && moveMade != Direction.Left)
            {
                GetMazeCell(row, column).WallRight = true;
            }
            //check move forward place wall back
            if (row + 1 < RowCount && !GetMazeCell(row + 1, column).IsVisited)
            {
                movesAvailable[movesAvailableCount] = Direction.Front;
                movesAvailableCount++;
            }
            else if (!GetMazeCell(row, column).IsVisited && moveMade != Direction.Back)
            {
                GetMazeCell(row, column).WallFront = true;
            }
            //check move left place wall right
            if (column > 0 && column - 1 >= 0 && !GetMazeCell(row, column - 1).IsVisited)
            {
                movesAvailable[movesAvailableCount] = Direction.Left;
                movesAvailableCount++;
            }
            else if (!GetMazeCell(row, column).IsVisited && moveMade != Direction.Right)
            {
                GetMazeCell(row, column).WallLeft = true;
            }
            //check move backward place wall foward
            if (row > 0 && row - 1 >= 0 && !GetMazeCell(row - 1, column).IsVisited)
            {
                movesAvailable[movesAvailableCount] = Direction.Back;
                movesAvailableCount++;
            }
            else if (!GetMazeCell(row, column).IsVisited && moveMade != Direction.Front)
            {
                GetMazeCell(row, column).WallBack = true;
            }
            GetMazeCell(row, column).IsVisited = true;
           
            if (movesAvailableCount > 0)
            {
               
                switch (movesAvailable[Random.Range(0, movesAvailableCount)])
                {
                    case Direction.Start:
                        break;
                    case Direction.Right:
                        VisitCell(row, column + 1, Direction.Right);
                        break;
                    case Direction.Front:
                        VisitCell(row + 1, column, Direction.Front);
                        break;
                    case Direction.Left:
                        VisitCell(row, column - 1, Direction.Left);
                        break;
                    case Direction.Back:
                        VisitCell(row - 1, column, Direction.Back);
                        break;
                }
            }
            for (int i = movesAvailableCount; i > 0; i--)
            {
                //Debug.Log("Maze Cell has " + movesAvailable[i] + " Open direction");
            }
        } while (movesAvailableCount > 0);// unitll we run out of moves
       
    }
}