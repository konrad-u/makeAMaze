using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour
{
    public GameObject horzPedal; //= null;
    public GameObject vertPedal; //= null;
    public float horzRot, vertRot;
    public float pedalFactor = -10;
    public float rotFactor = 30f;

    void Start()
    {
        //Get References to the game pedals for applying a rotation in Update()
        //initialize all values for use
    }

    // Update is called once per frame
    void Update()
    {
        rotateBoard();


        // Get Input from Input.GetAxis
        // Stretch it from 0-1 to 0 - maxRotFactor

        // Apply a part of the rotation to this (and children) to rotate the plafield
        // Use Quaternion.Slerp und Quaternion.Euler for doing it
        // REM: Maybe you have to invert one axis to get things right visualy

        // Apply an exaggerated ammount of rotation to the pedals to visualize the players input
        // Make the rotation look right

    }

    void rotateBoard()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("Up Arrow key was pressed.");S
            transform.Rotate(new UnityEngine.Vector3(1, 0, 0), -vertRot);
            vertPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), -vertRot);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Debug.Log("Up Arrow key was released.");
            transform.Rotate(new UnityEngine.Vector3(1, 0, 0), vertRot);
            vertPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), vertRot);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("Down Arrow key was pressed.");
            transform.Rotate(new UnityEngine.Vector3(1, 0, 0), vertRot);
            vertPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), vertRot);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("Down Arrow key was released.");
            transform.Rotate(new UnityEngine.Vector3(1, 0, 0), -vertRot);
            vertPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), -vertRot);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("Left Arrow key was pressed.");
            transform.Rotate(new UnityEngine.Vector3(0, 0, 1), -horzRot);
            horzPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), horzRot);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //Debug.Log("Left Arrow key was released.");
            transform.Rotate(new UnityEngine.Vector3(0, 0, 1), horzRot);
            horzPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), -horzRot);
        }

        //Detect when the down arrow key is pressed down
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("Right Arrow key was pressed.");
            transform.Rotate(new UnityEngine.Vector3(0, 0, 1), horzRot);
            horzPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), -horzRot);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Debug.Log("Right Arrow key was released.");
            transform.Rotate(new UnityEngine.Vector3(0, 0, 1), -horzRot);
            horzPedal.transform.Rotate(new UnityEngine.Vector3(0, -2, 0), horzRot);
        }
    }
    private void LateUpdate()
    {
        //resetting y rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,0f, transform.rotation.eulerAngles.z);
    }
}
