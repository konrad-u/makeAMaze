using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject floorCell;
    private GameObject[] neighborCells; // 0 = above, 1 = right, 2 = below, 3 = left, with z axis pointing up and down
    private Boolean visited;
    void Start()
    {
        visited = false;
        neighborCells = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setFloorCell(GameObject floorCell)
    {
        this.floorCell = floorCell;
    }
    public GameObject getFloorCell()
    {
        return floorCell;
    }
    public void setNeighbor(int direction, GameObject neighbor)
    {
        neighborCells[direction] = neighbor;
    }

    public GameObject getNeighbor(int direction)
    {
        return neighborCells[direction];
    }
}
