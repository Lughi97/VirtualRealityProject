using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour
{
    //use unidirectiona edge (keep track of the edges of the cell
    public MazeCell cell, nextCell;// keep track of connection between cell
    public MazeDirection direction;//orientation

    //intialize the edge as child of the cell 
    public void initialize(MazeCell cell, MazeCell nextCell, MazeDirection direction)
    {
        this.cell = cell;
        this.nextCell = nextCell;
        this.direction = direction;
        cell.setEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.toRotation();
    }
}
