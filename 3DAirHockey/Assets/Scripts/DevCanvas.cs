using UnityEngine;

/* Author: Malin Ejdbo
 * Last change date: 2018-04-13
 * Checked by:
 * Date of check:
 * Comment:
*/

//Enables or disables the developer interface canvas when the key D is pressed on the keyboard
public class DevCanvas : MonoBehaviour {
    //Variables
    public bool active = false; //Is the developer Interface active from start?
    private Canvas devCanvas;   //The canvas to be activated/deactivated

    // Use this for initialization
	void Start () {
        devCanvas = GetComponent<Canvas>();
        devCanvas.enabled = active;
    }
	
	// Update is called once per frame
	void Update () {
        //Check if the key is pressed
		if(Input.GetKeyDown(KeyCode.D))
        {
            //Deactivate or activate, like a toggle
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
