using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Single maze cell with all attributes
/// </summary>
public enum Direction
{
    Start,
    Right,
    Front,
    Left,
    Back
}
public class MazeCell
{
    //informations about the cell
    public bool IsVisited = false;
    public bool WallRight = false;
    public bool WallFront = false;
    public bool WallLeft = false;
    public bool WallBack = false;
    public bool IsGoal = false;
}
