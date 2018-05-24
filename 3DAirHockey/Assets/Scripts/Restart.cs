using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        if (SceneManager.GetActiveScene().name == "Classic")
            SceneManager.LoadScene("Neon");
        if (SceneManager.GetActiveScene().name == "Neon")
            SceneManager.LoadScene("Fotboll");
        if (SceneManager.GetActiveScene().name == "Fotboll")
            SceneManager.LoadScene("Candy");
        if (SceneManager.GetActiveScene().name == "Candy")
            SceneManager.LoadScene("Classic");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
