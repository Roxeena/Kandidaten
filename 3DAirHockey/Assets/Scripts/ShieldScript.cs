using UnityEngine;
using UnityEngine.UI;
/* Author: Malin Ejdbo
 * Last change date: 2018-04-25
 * Day of correction: 
 * Checked by: 
 * Date of check: 
 * Comment: 
*/

//Power up class that activates a shield around the goal of the player who picked up the power up object
//Deactivate after some time (can be changed in editor)
//Function to deactivate after a certain amount of hits implemented but does not work yet
public class ShieldScript : MonoBehaviour {
    //Variables
    public Collider ShieldRedGoal, ShieldBlueGoal;  //Shields
    public uint Lives = 1;                          //Number of hits a shield can take before breaking 
    private uint RedLives, BlueLives;               //Number of lives for each shield
    private bool RedShieldUp, BlueShieldUp;         //bool that says is a shield is up or not 
    private double precision = 0.01;                //How close to zero to say the time is zero (comparing floats)
    public Material Whole, Damaged;                 //Different material on the shield depending on if it is damaged and not

    //Use this for initialization
    void Start()
    {
        RedLives = BlueLives = Lives;
    }

    //Function to activate a shield based on who picked up the power up object
    public void activateShield(bool didRedPickUp)
    {
        //If a player with an already active shield picks up another shield object, extend the number of lives one step instead of stacking the power ups
        if(didRedPickUp && RedShieldUp)
        {
            ++RedLives;
            ShieldRedGoal.GetComponent<Renderer>().material = Whole;
        }
        else if(!didRedPickUp && BlueShieldUp)
        {
            ++BlueLives;
            ShieldBlueGoal.GetComponent<Renderer>().material = Whole;
        }

        //Depending on who picked up the object activate the shield for that person
        if (didRedPickUp && !RedShieldUp)
        {
            RedLives = Lives;   //Reset the number of lives on the shield
            ShieldRedGoal.GetComponent<Renderer>().material = Whole;
            ShieldRedGoal.gameObject.SetActive(true);
            RedShieldUp = true;
        }
        else if(!didRedPickUp && !BlueShieldUp)
        {
            BlueLives = Lives;  //Reset number of lives for the shield
            ShieldBlueGoal.GetComponent<Renderer>().material = Whole;
            ShieldBlueGoal.gameObject.SetActive(true);
            BlueShieldUp = true;
        }
    }

    //Function that deactivates the shield, after some time or after a number of hits
    //Red is an argument that says if it is the red sheild or the blue shield that are to be deactivated. 
    //true is red and false is blue
    public void deactivateShield(bool Red)
    {
        //Deactivate the shield told by argument Red 
        if (Red)
        {
            ShieldRedGoal.gameObject.SetActive(false);
            RedShieldUp = false;
        } 
        else
        {
            ShieldBlueGoal.gameObject.SetActive(false);
            BlueShieldUp = false;
        }     
    }

    //Decrement the number of lives the shield has left
    //Red is an argument that says if it is the red sheild or the blue shield that are to be decremented. 
    //true is red and false is blue
    public void decrement(bool Red)
    {
        // Decrement number of lives of the shield told by argument Red
        if (Red)
        {
            --RedLives;
            ShieldRedGoal.GetComponent<Renderer>().material = Damaged;
        }    
        else
        {
            --BlueLives;
            ShieldBlueGoal.GetComponent<Renderer>().material = Damaged;
        }

        // Check if shield is dead, then deactivate it
        if (RedLives == 0 || BlueLives == 0)
            deactivateShield(Red);  //If red is true, deactivate red shield. If false, deactivate blue shield
    }
}
