using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class CreateLevel : MonoBehaviour
{
    //The tiles have been modeled as 4x4 unity unit squares
    private const float tileSize = 4;    

    private GameObject root, floor, environment, edge;
    public int xHalfExt;
    public int zHalfExt;

    public GameObject topEdge, rightEdge, bottomEdge, leftEdge, basementOverlay, basementFloor, horzPedal, vertPedal, ball;

    public GameObject outerWall;
    public GameObject innerWall;    
    public GameObject exitTile;
    public GameObject[] floorTiles;  

    //private int start, end;
    private Vector3 startPos;
    private float xExt;
    private float zExt;
    private float envScaleFactor;
    private float xScale;
    private float zScale;

    public MazeCell[,] maze;

    // Use this for initialization
    void Awake()
    {
        // Gather together all refrences you will need later on

        root = GameObject.Find("MovablePlayfield");
        floor = GameObject.Find("DSBasementFloor");
        environment = GameObject.Find("Environment");

        xExt = (xHalfExt * 2 + 1);
        zExt = (zHalfExt * 2 + 1);
        envScaleFactor = 7;
        xScale = xExt / envScaleFactor;
        zScale = zExt / envScaleFactor;

        maze = new MazeCell[(int)xExt, (int)zExt];

        //scaleFloorAndCreateEdges();

        // Build an offset for the dyn playfield from the BasePlatform e.g. the bigger halfExtent value in unity units

        createFloor();

        //setPedals();

        // Scale Environment

        if (xHalfExt > zHalfExt)
        {
            environment.transform.localScale = new Vector3(xScale, xScale, xScale);
        }
        else
        {
            environment.transform.localScale = new Vector3(zScale, zScale, zScale);
        }
        floor.GetComponent<Transform>().localScale = new Vector3(xExt * tileSize + 1, 1, zExt * tileSize + 1);

        // Scale  + position BasePlate

        if (root != null)    
        {
            // Create the outer walls for given extXZ
            createOuterWalls();
            // create a maze


            //setupNeighbors(maze);


            // Build the maze from the given set of prefabs
            // Set the walls for the maze (place only one wall between two cells, not two!)

            // Place the PlayerBall above the playfield
            placeBallStart();
            setBallAtStart();
        }
    }

    //You might need this more than once...
    void placeBallStart()
    {
        float randomX = UnityEngine.Random.Range(-xHalfExt * tileSize, xHalfExt * tileSize);
        float randomZ = UnityEngine.Random.Range(-zHalfExt * tileSize, zHalfExt * tileSize);
        startPos = new Vector3(randomX, 5, randomZ);
    }

    public void setBallAtStart()
    {
        ball.transform.position = startPos;
    }

    public void EndzoneTrigger(GameObject other)
    {    

        // Check if ball first...
        // Player has fallen onto ground plane, reset
    }
    public void winTrigger(GameObject other)
    {    
        // Check if ball first...

        // Destroy this maze
        // Generate new maze
        // Player has fallen onto ground plane, reset
    }

    /*no longer used, resolved scaling issues by scaling the environment as a whole 
     * 
    public void scaleFloorAndCreateEdges()
    {
        float xEdgeOffset = (2*tileSize * (xHalfExt + 1));
        float xEdgeLength = 2*tileSize * (2 * xHalfExt + 1)+tileSize*2;
        float zEdgeOffset = (2*tileSize * (zHalfExt + 1));
        float zEdgeLength = 2*tileSize * (2 * zHalfExt + 1) + tileSize * 2;

        floor.transform.localScale = new Vector3(tileSize * (2* xHalfExt + 1), 1, tileSize * (2 * zHalfExt + 1));

        topEdge.transform.localPosition = new Vector3(0, -5, -zEdgeOffset);
        topEdge.transform.localScale = new Vector3(0.2f , tileSize, xEdgeLength);

        bottomEdge.transform.localPosition = new Vector3(0, -5, zEdgeOffset);
        bottomEdge.transform.localScale = new Vector3(0.2f, tileSize, xEdgeLength);

        leftEdge.transform.localPosition = new Vector3(-xEdgeOffset, -5, 0);
        leftEdge.transform.localScale = new Vector3(0.2f, tileSize, zEdgeLength);

        rightEdge.transform.localPosition = new Vector3(xEdgeOffset, -5, 0);
        rightEdge.transform.localScale = new Vector3(0.2f, tileSize, zEdgeLength);

        basementOverlay.transform.localScale = new Vector3(xEdgeLength/10, 0, zEdgeLength/10) ;
        basementFloor.transform.localScale = new Vector3(xEdgeLength/2.2f, 1, zEdgeLength/2.2f);

    }
    */

    public void createFloor()
    {
        int randomI = UnityEngine.Random.Range(-xHalfExt, xHalfExt);
        int randomJ = UnityEngine.Random.Range(-zHalfExt, zHalfExt);
        GameObject createdTile;

        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            for (int j = -zHalfExt; j <= zHalfExt; j++)
            {
                if (i.Equals(randomI) && j.Equals(randomJ))
                {
                    createdTile = Instantiate(exitTile, new Vector3(i, 0, j) * tileSize + new Vector3(0, 0.5f, 0),
                    Quaternion.Euler(0, 0, 0),
                    root.transform);
                }
                else
                {
                    int rTile = UnityEngine.Random.Range(0, 6);
                    createdTile = Instantiate(floorTiles[rTile],
                        new Vector3(i, 0, j) * tileSize + new Vector3(0, 0.5f, 0),
                        Quaternion.Euler(0, 0, 0),
                        root.transform);
                }
                int mazeCol = i + xHalfExt;
                int mazeRow = j + zHalfExt;
                maze[mazeCol, mazeRow] = gameObject.AddComponent<MazeCell>();
                maze[mazeCol, mazeRow].floorTile = createdTile;


                Debug.Log(maze[mazeCol, mazeRow] + " at " + maze[mazeCol, mazeRow].floorTile.transform.position + ", col "+ mazeCol + ", row " + mazeRow);
            }
        }
    }


    public void setupNeighbors(MazeCell[,] maze)
    {
        int totNeighbors = 0;
        for (int i = 0; i < xExt; i++)
        {
            for (int j = 0; j < zExt; j++)
            {

                float tileX = maze[i, j].transform.position.x;
                float tileY = maze[i, j].transform.position.y;
                float tileZ = maze[i, j].transform.position.z;
                GameObject parentTile = maze[i, j].floorTile;

                if (i >= 1)
                {
                    maze[i, j].westNeighbor = maze[i-1, j];
                    Debug.Log("west " + maze[i, j].westNeighbor);
                    totNeighbors++;


                   maze[i, j].westWall = Instantiate(innerWall, new Vector3(tileX + tileSize / 2, tileY, tileZ), Quaternion.Euler(0, 90, 0), parentTile.transform);
                }
                if(i < xExt-1)
                {
                    maze[i, j].eastNeighbor = maze[i + 1, j];
                    Debug.Log("west " + maze[i, j].eastNeighbor);
                    totNeighbors++;

                    maze[i, j].eastWall = Instantiate(innerWall, new Vector3(tileX - tileSize / 2, tileY, tileZ), Quaternion.Euler(0, 90, 0), parentTile.transform);
                }
                if (j >= 1)
                {
                    maze[i, j].northNeighbor = maze[i, j - 1];
                    Debug.Log("west " + maze[i, j].northNeighbor);
                    totNeighbors++;

                    maze[i, j].northWall = Instantiate(innerWall, new Vector3(tileX, tileY, tileZ - tileSize / 2), Quaternion.Euler(0, 0, 0), parentTile.transform);
                }
                if (j < zExt-1)
                {
                    maze[i, j].westNeighbor = maze[i, j+1];
                    Debug.Log("west " + maze[i, j].westNeighbor);
                    totNeighbors++;

                    maze[i, j].southWall = Instantiate(innerWall, new Vector3(tileX, tileY, tileZ + tileSize / 2), Quaternion.Euler(0, 0, 0), parentTile.transform);
                }
            }
        }
        Debug.Log(totNeighbors);
    }


    public void createOuterWalls()
    {

        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(i * tileSize, 0, zHalfExt * tileSize) + new Vector3(0, 1, tileSize / 2),
                        Quaternion.Euler(0, 0, 0),
                        root.transform);
        }
        for (int i = -zHalfExt; i <= zHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(xHalfExt * tileSize, 0, i * tileSize) + new Vector3(tileSize / 2, 1, 0),
                        Quaternion.Euler(0, 90, 0),
                        root.transform);
        }
        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(i * tileSize, 0, -zHalfExt * tileSize) + new Vector3(0, 1, -tileSize / 2),
                        Quaternion.Euler(0, 0, 0),
                        root.transform);
        }
        for (int i = -zHalfExt; i <= zHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(-xHalfExt * tileSize, 0, i * tileSize) + new Vector3(-tileSize / 2, 1, 0),
                        Quaternion.Euler(0, 90, 0),
                        root.transform);
        }

    }

    /* also no longer used
    public void setPedals()
    {
        horzPedal.transform.position = new Vector3(0, horzPedal.transform.position.y, (zHalfExt * tileSize)+tileSize + horzPedal.transform.localScale.z);

        vertPedal.transform.position = new Vector3((xHalfExt * tileSize) + tileSize + vertPedal.transform.localScale.x, vertPedal.transform.position.y, 0);
    }
    */

}
	

