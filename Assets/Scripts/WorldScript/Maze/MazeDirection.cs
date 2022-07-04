using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MazeDirection
{
    North,
    East,
    South,
    West
}

public static class MazeDirections
{
    public const int count = 4;// how many direcitons


    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    public static MazeDirection getOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }
    public static IntVector2[] vectorsDirection =
    {
        new IntVector2(0,1),//North
        new IntVector2(1,0),//East
        new IntVector2(0,-1),//South
        new IntVector2(-1,0),//West
    };


    public static MazeDirection RandomValue
    {
        get
        {
            //Randomize the direciton of the cells
            return (MazeDirection)Random.Range(0, count);
        }
    }

    //convert the direction in to integer
    public static IntVector2 toIntVector2(this MazeDirection direction)
    {
        return vectorsDirection[(int)direction];
    }


    //add the rotations to passages and walls
    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    public static Quaternion toRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

}
