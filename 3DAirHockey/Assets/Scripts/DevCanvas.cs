using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCanvas : MonoBehaviour {

    public bool active = false;
    private Canvas devCanvas;

    // Use this for initialization
	void Start () {
        devCanvas = GetComponent<Canvas>() ;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (active)
            {
                devCanvas.enabled = false;
                active = false;
            }
            else
            {
                devCanvas.enabled = true;
                active = true;
            }    
        }
	}
}
