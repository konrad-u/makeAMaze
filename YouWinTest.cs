using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class YouWinTest : MonoBehaviour {
    CreateLevel gameCtrl;    

    // Use this for initialization
    void Start()
    {
        gameCtrl = FindObjectOfType<CreateLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        // call a method of gameCtrl indicating a possible win situation
        // Is ist good to test for Player here?
        print("You Won!");
        SceneManager.LoadScene("s0572411_KonradUkens");
    }
}
