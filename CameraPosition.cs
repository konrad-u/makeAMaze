using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject manager, frameObject;
    public int rel;
    RectTransform BOBounds;

    // Start is called before the first frame update
    void Start()
    {
        //https://answers.unity.com/questions/1190535/auto-scale-camera-to-fit-game-object-to-screen-cen.html
        Vector3 xyz = frameObject.GetComponent<MeshFilter>().mesh.bounds.size;
        float distance = Mathf.Max(xyz.x, xyz.y, xyz.z);
        distance /= (2.0f * Mathf.Tan(0.5f * Camera.fieldOfView * Mathf.Deg2Rad));
        // Move camera in -z-direction; change '2.0f' to your needs
        yourCamera.transform.position = new Vector3(yourCamera.transform.position.x, yourCamera.transform.position.y, -distance * 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
