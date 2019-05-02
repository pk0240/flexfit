using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.IO;


public class DBConnection : MonoBehaviour {

    SpeedUpgrade sp;

    //public TextAsset file;
    void Start()
    {
        sp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpeedUpgrade>();
        //TextAsset txt = (TextAsset)Resources.Load("saveData");
        //txtContents = txt.text;

    }// Start()
    
    public void readData(string dataType)
    {
        //StartCoroutine(sendTextToFile());
        StartCoroutine(pullFromServer(dataType));
    }

    IEnumerator pullFromServer(string fieldset)
    {
        bool successful = true;
        WWWForm form = new WWWForm();
        form.AddField("field", fieldset);
        WWW www = new WWW(@"http://localhost:9000/tounity.php", form);
        yield return www;

        if (www.error != null) successful = false;
        else
        {
            /*
            successful = true;

            if (fieldset.Equals("initialisation"))
                PlayerPrefs.SetInt("initialisation", Convert.ToInt32(www.text)); 
            if (fieldset.Equals("steps"))
                sp.tempSteps = Convert.ToInt32(www.text);
            if (fieldset.Equals("speed"))
                sp.tempSpeed = Convert.ToInt32(www.text);
            if (fieldset.Equals("jump"))
                sp.tempJump = Convert.ToInt32(www.text);

            Debug.Log("TempSteps: " +sp.tempSteps);*/
        }

    }


    public void updateDB(string field, string value)
    {
        StartCoroutine(postToServer(field, value));
    }

    IEnumerator postToServer(string field, string value)
    {
        Debug.Log("Hello");
        bool successful = true;
        WWWForm form = new WWWForm();
        
        form.AddField("steps", value);
        form.AddField("speed", value);
        form.AddField("jump", value);
        form.AddField("initialisation", value);
        form.AddField("update", field);

        WWW www = new WWW(@"http://localhost:9000/fromunity.php", form);
        yield return www;

        
        if (www.error != null) successful = false;
        else { successful = true; Debug.Log("Success! " + www.text); }
    }

   
}

