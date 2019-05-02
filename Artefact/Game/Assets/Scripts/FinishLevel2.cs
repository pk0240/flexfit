using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel2 : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.name == "Player")
        {

            SceneManager.LoadScene("2ndLevelWin");

            //Or:
            //SceneManager.LoadScene (SceneIndex); //(without these: ", because it's a number - an int, not a string)
        }
    }
}
