using UnityEngine;

/* Author: Emilie Ho && Malin Ejdbo
 * Last change date: 2018-04-25
 * Checked by: 
 * Date of check: 
 * Comment: 
*/

//Power up class that expands the striker for the player who picked up the power up object
//Deactivate after some time (can be changed in editor)
public class ExpandScript : MonoBehaviour
{
    //Variables
    public Rigidbody RedStriker;                    //Red striker
    public Rigidbody BlueStriker;                   //Blue striker
    public uint PUpTime = 5;                        //Time expand is active
    private float ExpandTimeRed, ExpandTimeBlue;    //Time left for each expand
    private bool RedExpandOn, BlueExpandOn;         //bool that says is a expand is up or not 
    private double precision = 0.01;                //How close to zero to say the time is zero (comparing floats)

    //Use this for initialization
    void Start()
    {
        ExpandTimeRed = ExpandTimeBlue = PUpTime;
    }

    //Function to activate the expand based on who picked up the power up object
    public void activateExpand(bool didRedPickUp)
    {
        //If a player with an already active expand picks up another expand object, extend the time instead of stacking the power ups
        if (didRedPickUp && RedExpandOn)
            ExpandTimeRed += PUpTime;
        else if (!didRedPickUp && BlueExpandOn)
            ExpandTimeBlue += PUpTime;

        //Depending on who picked up the object activate the expand for that striker
        if (didRedPickUp && !RedExpandOn)
        {
            ExpandTimeRed = PUpTime;
            RedStriker.gameObject.transform.localScale *= 1.5f; 
            RedExpandOn = true;
        }
        else if(!didRedPickUp && !BlueExpandOn)
        {
            ExpandTimeBlue = PUpTime;
            BlueStriker.gameObject.transform.localScale *= 1.5f;
            BlueExpandOn = true;
        }
    }

    //Function that deactivates the expand after some time
    public void deactivateExpand(bool Red)
    {
        //Deactivate the expand told by argument Red 
        if (Red)
        {
            RedStriker.gameObject.transform.localScale *= 0.66f;
            RedExpandOn = false;
        }
        else
        {
            BlueStriker.gameObject.transform.localScale *= 0.66f;
            BlueExpandOn = false;
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Update the time left on the expands if they are up
        if (RedExpandOn)
        {
            ExpandTimeRed -= Time.deltaTime;
            //Check if the expand time has run out and deactivate it
            if (ExpandTimeRed <= precision)
            {
                deactivateExpand(true);
            }
        }
        else if (BlueExpandOn)
        {
            ExpandTimeBlue -= Time.deltaTime;
            //Check if the expand time has run out and deactivate it
            if (ExpandTimeBlue <= precision)
            {
                deactivateExpand(false);
            }
        }
    }
}