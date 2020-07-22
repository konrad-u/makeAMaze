using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour
{
    public GameObject horzPedal; //= null;
    public GameObject vertPedal; //= null;
    public float horzRot, vertRot;
    public float pedalFactor = -10;
    public float rotFactor = 30f;
    public float tiltSpeed;

    void Start()
    {
        if (horzPedal == null)
        {
            horzPedal = GameObject.Find("HorzPedal");
        }
        if (vertPedal == null)
        {
            vertPedal = GameObject.Find("VertPedal");
        }
        tiltSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input from Input.GetAxis
        // Stretch it from 0-1 to 0 - maxRotFactor
        horzRot = Input.GetAxis("Horizontal") * rotFactor;
        vertRot = Input.GetAxis("Vertical") * rotFactor;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(vertRot, 0, -horzRot), Time.deltaTime * tiltSpeed);

        // Apply an exaggerated ammount of rotation to the pedals to visualize the players input
        // Make the rotation look right
        horzPedal.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3((horzPedal.GetComponent<Transform>().eulerAngles.x + pedalFactor) * Input.GetAxis("Horizontal"), 90, 90));
        vertPedal.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3((vertPedal.GetComponent<Transform>().eulerAngles.x + pedalFactor) * Input.GetAxis("Vertical"), 0, 90));
    }
}
