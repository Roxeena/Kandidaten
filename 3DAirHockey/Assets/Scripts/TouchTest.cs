using UnityEngine;
using UnityEngine.UI;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-04-13
 * Comment: Is it needed? Dokumentation.
*/

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
