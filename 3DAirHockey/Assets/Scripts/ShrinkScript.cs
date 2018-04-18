using UnityEngine;

/* Author: Emilie Ho
 * Last change date: 2018-04-17
 * Checked by: Malin Ejdbo
 * Date of check: 2018-04-17
 * Comment: Looks good!
*/

//Power up class that shrinks the striker for the player who picked up the power up object
//Deactivate after some time (can be changed in editor)
public class ShrinkScript : MonoBehaviour
{
    //Variables
    public Rigidbody RedStriker;                    //Red striker
    public Rigidbody BlueStriker;                   //Blue striker
    public uint PUpTime = 5;                        //Time shrink is active
    private float ShrinkTimeRed, ShrinkTimeBlue;    //Time left for each shrink
    private bool RedShrinkOn, BlueShrinkOn;         //bool that says is a shrink is up or not 
    private double precision = 0.01;                //How close to zero to say the time is zero (comparing floats)

    //Use this for initialization
    void Start()
    {
        ShrinkTimeRed = ShrinkTimeBlue = PUpTime;
    }

    //Function to activate the shrink based on who picked up the power up object
    public void activateShrink(bool didRedPickUp)
    {
        //Depending on who picked up the object activate the shrink for that striker
        if (didRedPickUp)
        {
            RedStriker.gameObject.transform.localScale *= 0.5f;
            RedShrinkOn = true;
        }
        else
        {
            BlueStriker.gameObject.transform.localScale *= 0.5f;
            BlueShrinkOn = true;
        }
    }

    //Function that deactivates the shrink after some time
    public void deactivateShrink(bool Red)
    {
        //Deactivate the shrink told by argument Red 
        if (Red)
        {
            RedStriker.gameObject.transform.localScale *= 2f;
            RedShrinkOn = false;
        }
        else
        {
            BlueStriker.gameObject.transform.localScale *= 2f;
            BlueShrinkOn = false;
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Update the time left on the shrinks if they are up
        if (RedShrinkOn)
        {
            ShrinkTimeRed -= Time.deltaTime;
            //Check if the shrinks time has run out and deactivate it
            if (ShrinkTimeRed <= precision)
            {
                deactivateShrink(true);
            }
        }
        else if (BlueShrinkOn)
        {
            ShrinkTimeBlue -= Time.deltaTime;
            //Check if the shrinks time has run out and deactivate it
            if (ShrinkTimeBlue <= precision)
            {
                deactivateShrink(false);
            }
        }
    }
}