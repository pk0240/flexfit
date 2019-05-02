using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    void Update()
    {
        transform.Rotate(0, 3, 0 * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
