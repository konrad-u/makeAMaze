using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject frameObject;
    private Vector3 xyztemp;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //https://answers.unity.com/questions/1190535/auto-scale-camera-to-fit-game-object-to-screen-cen.html

        //Vector3 xyz = frameObject.GetComponent<MeshFilter>().mesh.bounds.size;
        Vector3 xyz = frameObject.transform.localScale;

        float distance = Mathf.Max(xyz.x, xyz.y, xyz.z);
        Debug.Log("frameObject is " + frameObject + ", distance is " + distance);

        distance /= (2.0f * Mathf.Tan(0.5f * cam.fieldOfView * Mathf.Deg2Rad));
        Debug.Log("the new distance is " + distance);
        // Move camera in -z-direction; change '2.0f' to your needs
        //cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -distance * 2.0f);
        cam.fieldOfView = cam.fieldOfView * (distance/5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
