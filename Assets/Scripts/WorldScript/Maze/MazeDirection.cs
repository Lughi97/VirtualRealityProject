using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MazeDirection
{
    North,//(0,1)
    East,//(1,0)
    South,//(0,-1)
    West,//(-1,0)

}
public static class MazeDirections
{
    public const int count = 4;// how many direcitons


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
   
}
