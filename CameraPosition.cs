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
        BOBounds = frameObject.GetComponent<RectTransform>();
        DisplayWorldCorners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DisplayWorldCorners()
    {
        Vector3[] v = new Vector3[4];
        BOBounds.GetWorldCorners(v);

        Debug.Log("World Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + v[i]);
        }
    }
}
