using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel3 : MonoBehaviour {

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Platform3");
    }
}
