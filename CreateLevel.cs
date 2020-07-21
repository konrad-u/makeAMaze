﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class CreateLevel : MonoBehaviour
{
    //The tiles have been modeled as 4x4 unity unit squares
    private const float tileSize = 4;    

    private GameObject root, floor, environment, ball;
    public int xHalfExt;
    public int zHalfExt;

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

        // Build an offset for the dyn playfield from the BasePlatform e.g. the bigger halfExtent value in unity units

        createFloor();

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
            Instantiate(outerWall, new Vector3(i * tileSize, 0, zHalfExt * tileSize) + new Vector3(0, 1f, tileSize / 2),
                        Quaternion.Euler(0, 0, 0),
                        root.transform);
        }

        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(xHalfExt * tileSize, 0, i * tileSize) + new Vector3(tileSize / 2, 1, 0),
                        Quaternion.Euler(0, 90, 0),
                        root.transform);
        }
        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(i * tileSize, 0, -zHalfExt * tileSize) + new Vector3(0, 1f, -tileSize / 2),
                        Quaternion.Euler(0, 0, 0),
                        root.transform);
        }

        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            Instantiate(outerWall, new Vector3(-xHalfExt * tileSize, 0, i * tileSize) + new Vector3(-tileSize / 2, 1, 0),
                        Quaternion.Euler(0, 90, 0),
                        root.transform);
        }

    }

    public void setupDSBasementFloor()
    {
        root.transform.localScale = new Vector3(20, 20, 20);
    }

}
	

