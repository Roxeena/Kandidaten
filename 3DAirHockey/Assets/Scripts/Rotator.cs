using UnityEngine;

/* Author: Malin Ejdbo
 * Last change date: 2018-04-13
 * Checked by:
 * Date of check:
 * Comment:
*/

//Simple class that rotates the object tha script is attached to
public class Rotator : MonoBehaviour {
    //Variables
    public int speed = 30;  //Determines how fast to rotatate
    private Rigidbody obj;  //Object to rotate
    
    //Use this for initialization
    void Start()
    {
        obj = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
        //Rotate around its own y axis
        transform.RotateAround(obj.position, Vector3.up, Time.deltaTime * speed);
	}
}
