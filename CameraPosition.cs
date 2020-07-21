using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject manager;
    public int rel;

    // Start is called before the first frame update
    void Start()
    {
        //int xHalfExt = manager.GetComponent<>.;
        //rel = manager.xHalfExt * 4;
        transform.Translate(new Vector3(1, 1, -(1*rel)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
