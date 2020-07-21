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
    private int start, end;

    // Use this for initialization
    void Awake()
    {
        // Gather together all refrences you will need later on

        root = GameObject.Find("MovablePlayfield");
        floor = GameObject.Find("DSBasementFloor");


        scaleFloorAndCreateEdges();

        // Build an offset for the dyn playfield from the BasePlatform e.g. the bigger halfExtent value in unity units

        createFloor();

        setPedals();
        // Calculate a scale factor for scaling the non-movable environment (and therefore the camera) and the BasePlatform 
        // the factors that the environment are scaled for right now are for x/zHalfExt =1, scale accordingly
        // i.e. the playfield/environment should be as big as the dynamic field

        // Scale Environment

        // Scale  + position BasePlate

        if (root != null)    
        {
            // Create the outer walls for given extXZ
            createOuterWalls();
            // create a maze
            // Build the maze from the given set of prefabs
            // Set the walls for the maze (place only one wall between two cells, not two!)

            // Place the PlayerBall above the playfield
            placeBallStart();
        }
    }

    //You might need this more than once...
    void placeBallStart()
    {
        float randomX = UnityEngine.Random.Range(-xHalfExt * tileSize, xHalfExt * tileSize);
        Debug.Log(randomX);
        float randomZ = UnityEngine.Random.Range(-zHalfExt * tileSize, zHalfExt * tileSize);
        Debug.Log(randomZ); 
        ball.transform.position = new Vector3(randomX, 5, randomZ);
        // Reset Physics
        // Place the ball
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
        Debug.Log("the x edge length is " + xEdgeLength);
        Debug.Log("the z edge length is " + zEdgeLength);

        basementOverlay.transform.localScale = new Vector3(xEdgeLength/10, 0, zEdgeLength/10) ;
        basementFloor.transform.localScale = new Vector3(xEdgeLength/2.2f, 1, zEdgeLength/2.2f);

    }

    public void createFloor()
    {
        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            for (int j = -zHalfExt; j <= zHalfExt; j++)
            {
                int rTile = UnityEngine.Random.Range(0, 6);
                Instantiate(floorTiles[rTile],
                    new Vector3(i, 0, j) * tileSize + new Vector3(0, 0.5f, 0),
                    Quaternion.Euler(0, 0, 0),
                    root.transform);
            }
        }
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

    public void setPedals()
    {
        horzPedal.transform.position = new Vector3(0, horzPedal.transform.position.y, (zHalfExt * tileSize)+tileSize + horzPedal.transform.localScale.z);

        vertPedal.transform.position = new Vector3((xHalfExt * tileSize) + tileSize + vertPedal.transform.localScale.x, vertPedal.transform.position.y, 0);
    }

}
	

