using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpeedUpgrade : MonoBehaviour {
    public static int steps;
    public static float speedlevel;
    public static float jumplevel;
    public static string init;

    public Text upgradespeedtext;
    public Text upgradejumptext;
    public Text StepCount;

    public DBConnection dbConn;
    
    void Start()
    {
        dbConn = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DBConnection>();

        StartCoroutine(pullFromServer("init"));
        StartCoroutine(pullFromServer("steps"));
        StartCoroutine(pullFromServer("speed"));
        StartCoroutine(pullFromServer("jump"));

        if(init == null)
        {
            upgradespeedtext.text = "[...]";
            upgradejumptext.text = "[...]";
            StepCount.text = "Syncing new game data ... \nPlease return to the main menu and try again.";
        }
        else
        {
            Debug.Log("Init: " + init);
            upgradespeedtext.text = "Level:" + speedlevel;
            upgradejumptext.text = "Level:" + jumplevel;
            StepCount.text = "Stepcount:" + steps;
        }
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
            successful = true;

            if (fieldset.Equals("init"))
                init = www.text;
            if (fieldset.Equals("steps"))
                steps = Convert.ToInt32(www.text);
            if (fieldset.Equals("speed"))
                speedlevel = Convert.ToInt32(www.text);
            if (fieldset.Equals("jump"))
                jumplevel = Convert.ToInt32(www.text);

        }

    }

    IEnumerator pushToServer(string field, string value)
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



    public void OnMouseDownSpeed()
    {

        if (steps >= 1000)
        {
            speedlevel = speedlevel + 1;
            upgradespeedtext.text = "Level:" + speedlevel;
            steps = steps - 1000;
            StepCount.text = "Stepcount:" + steps;

            // Pushes the data to the server to save to the DB
            StartCoroutine(pushToServer("steps", steps.ToString()));
            StartCoroutine(pushToServer("speed", speedlevel.ToString()));
        }
    }

    public void OnMouseDownSpeedRevert()
    {

        if (speedlevel > 1)
        {
            speedlevel = speedlevel - 1;
            upgradespeedtext.text = "Level:" + speedlevel;
            steps = steps + 1000;
            StepCount.text = "Stepcount:" + steps;

            StartCoroutine(pushToServer("steps", steps.ToString()));
            StartCoroutine(pushToServer("speed", speedlevel.ToString()));
        }
    }

    public void OnMouseDownJump()
    {
        
        if (steps >= 4000)
        {
            jumplevel = jumplevel + 1;
            upgradejumptext.text = "Level:" + jumplevel;
            steps = steps - 4000;
            StepCount.text = "Stepcount:" + steps;

            PlayerPrefs.SetInt("steps", steps);
            PlayerPrefs.SetFloat("jump", jumplevel);

            StartCoroutine(pushToServer("steps", steps.ToString()));
            StartCoroutine(pushToServer("jump", jumplevel.ToString()));
        }    
    }

    public void OnMouseDownJumpRevert()
    {
        if (jumplevel > 1)
        {
            jumplevel = jumplevel - 1;
            upgradejumptext.text = "Level:" + jumplevel;
            steps = steps + 4000;
            StepCount.text = "Stepcount:" + steps;

            PlayerPrefs.SetInt("steps", steps);
            PlayerPrefs.SetFloat("jump", jumplevel);

            StartCoroutine(pushToServer("steps", steps.ToString()));
            StartCoroutine(pushToServer("jump", jumplevel.ToString()));
        }
    }


}
