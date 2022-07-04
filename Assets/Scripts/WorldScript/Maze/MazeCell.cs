using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public IntVector2 coordinates;

    private int initializedEdgeCount;

    public bool isFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirections.count;
        }
    }

    //it's 4 edges in the 4 directions
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.count];// the 4 edges

    public MazeCellEdge getEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    public void setEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }
    public MazeDirection randomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }


}
