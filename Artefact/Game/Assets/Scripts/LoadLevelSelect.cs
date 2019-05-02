using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelect : MonoBehaviour {

    public void OnMouseDown()
    {
        SceneManager.LoadScene("SelectLevel");
    }

}
