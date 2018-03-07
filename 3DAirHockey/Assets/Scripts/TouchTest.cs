using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour {

    public Text antal;

    // Use this for initialization
    void Start () {
        antal.text = "0";
    }
	
	// Update is called once per frame
	void Update () {


        antal.text = (Input.touchCount).ToString();
    }
}
