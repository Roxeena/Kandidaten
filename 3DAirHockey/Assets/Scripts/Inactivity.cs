using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Inactivity : MonoBehaviour {
    private float LastActiveTimeStamp = 0.0f;
    private float currentTimeStamp = 0.0f;

    public float inactiveTime = 30.0f;

	// Use this for initialization
	void Start () {
		
	}

    public void UpdateActivity(float time)
    {
        Debug.Log("Activity!");
        LastActiveTimeStamp = time;
    }
	
	// Update is called once per frame
	void Update () {
        //Update the curretn time stamp
        currentTimeStamp += Time.deltaTime;

        //Compare to the last active time stamp
        if( inactiveTime < Mathf.Abs(currentTimeStamp - LastActiveTimeStamp) )
        {
            Debug.Log("Inactive, go to menu");
            //If the inactivity has been to long then activate the start menu again
            //Ask before maybe?
            SceneManager.LoadScene("menu");
        } 
	}
}
