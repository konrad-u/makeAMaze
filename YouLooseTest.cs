using UnityEngine;
using System.Collections;

public class YouLooseTest : MonoBehaviour
{

    CreateLevel gameCtrl;   

    // Use this for initialization
    void Start()
    {
        gameCtrl = FindObjectOfType<CreateLevel>();
    }

     void OnTriggerEnter(Collider other)
    {
        // call a method of gameCtrl
        print("You lost.");
        gameCtrl.setBallAtStart();
    }
}
