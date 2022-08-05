using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator{
    //used to obtain the rows and coloms of the private variable

    //Used to obtain the Row and Column from the private variables 
    public int RowCount { get { return mMazeRows; } }
    public int ColumnCount { get { return mMazeColumns; } }
    private int mMazeRows;
    private int mMazeColumns;
    private MazeCell[,] mMaze;
    //A constructor that makes the rows and columns non-zero
    //and instantiates a new MazeCell at that specific rank and range
    public MazeGenerator(int rows, int columns)
    {
        mMazeRows = Mathf.Abs(rows);
        mMazeColumns = Mathf.Abs(columns);
        if (mMazeRows == 0)
        {
            mMazeRows = 1;
        }
        if (mMazeColumns == 0)
        {
            mMazeColumns = 1;
        }
        mMaze = new MazeCell[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                mMaze[row, column] = new MazeCell();
            }
        }
    }
    //called by the algorithm class to start the algorithm
    public abstract void GenerateMaze();
    public MazeCell GetMazeCell(int row, int column)
    {
        if (row >= 0 && column >= 0 && row < mMazeRows && column < mMazeColumns)
        {
            return mMaze[row, column];
        }
        else
        {
            Debug.LogError(row + " " + column);
            throw new System.ArgumentOutOfRangeException();
        }
    }

}