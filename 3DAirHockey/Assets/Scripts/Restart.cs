using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//restarts the game if "r" is pressed
        {
            Reload_scene();
        }
    }

    public void Reload_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
