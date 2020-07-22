using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floorCell;
    public GameObject northNeighbor, eastNeighbor, southNeighbor, westNeighbor;
    public GameObject northWall, eastWall, southWall, westWall;
    public Boolean visited;
}
