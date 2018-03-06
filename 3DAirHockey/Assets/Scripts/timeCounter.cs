using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour {

    public Text timerTxt;


	// Use this for initialization
	void Start () {
        timerTxt.text = "0";		
	}
	
	// Update is called once per frame
	void Update () {

        timerTxt.text = (Time.time).ToString();
		
	}
}
