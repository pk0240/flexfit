using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {

        
        if (collision.gameObject.name == "Player")
        {
            
            SceneManager.LoadScene("LevelWin");
            

            //Or:
            //SceneManager.LoadScene (SceneIndex); //(without these: ", because it's a number - an int, not a string)
        }
    }
  
}
