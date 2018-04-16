using UnityEngine;
using UnityEngine.UI;
/* Author: Malin Ejdbo
 * Last change date: 2018-04-13
 * Checked by: Emilie Ho
 * Date of check: 2018-04-16
 * Comment: Add what "red" means in deactivateShield.
*/

//Power up class that activates a shield around the goal of the player who picked up the power up object
//Deactivate after some time (can be changed in editor)
//Function to deactivate after a certain amount of hits implemented but does not work yet
public class ShieldScript : MonoBehaviour {
    //Variables
    public Collider ShieldRedGoal, ShieldBlueGoal;  //Shields
    public uint PUpTime = 10;                       //Time shield is active
    public uint Lives = 3;                          //Number of hits a shield can take before breaking 
    private uint RedLives, BlueLives;               //Number of lives for each shield
    private float ShieldTimeRed, ShieldTimeBlue;    //Time left for each shield
    private bool RedShieldUp, BlueShieldUp;         //bool that says is a shield is up or not 
    public Text RedShieldTxt, BlueShieldTxt;        //UI text to show how much time is left
    private float numDecimals = 1f;                 //How many decimals of he time to be displayed
    private double precision = 0.01;               //How close to zero to say the time is zero (comparing floats)

    //Use this for initialization
    void Start()
    {
        ShieldTimeRed = ShieldTimeBlue = PUpTime;
        RedLives = BlueLives = Lives;
    }

    //Function to activate a shield based on who picked up the power up object
    public void activateShield(bool didRedPickUp)
    {
        //Depending on who picked up the object activate the shield for that person
        if (didRedPickUp)
        {
            ShieldRedGoal.gameObject.SetActive(true);
            RedShieldUp = true;
            RedShieldTxt.gameObject.SetActive(true);
        }
        else
        {
            ShieldBlueGoal.gameObject.SetActive(true);
            BlueShieldUp = true;
            BlueShieldTxt.gameObject.SetActive(true);
        }
    }

    //Function that deactivates the shield, after some time or after a number of hits
    public void deactivateShield(bool Red)
    {
        //Deactivate the shield told by argument Red 
        if (Red)
        {
            ShieldRedGoal.gameObject.SetActive(false);
            RedShieldUp = false;
            RedShieldTxt.gameObject.SetActive(false);
        } 
        else
        {
            ShieldBlueGoal.gameObject.SetActive(false);
            BlueShieldUp = false;
            BlueShieldTxt.gameObject.SetActive(false);
        }     
    }

    //Decrement the number of lives the shield has left
    public void decrement(bool Red)
    {
        // Decrement number of lives of the shield told by argument Red
        if (Red)
            --RedLives;
        else
            --BlueLives;

        // Check if shield is dead, then deactivate it
        if (RedLives == 0 || BlueLives == 0)
            deactivateShield(Red);
    }

    //Update is called once per frame
    void Update()
    {
        //Update the time left on the shields if they are up
        //Update also the interface time so users can se how much time it is left, take away all the decimals 
        if (RedShieldUp)
        {
            ShieldTimeRed -= Time.deltaTime;
            RedShieldTxt.text = Mathf.Round(ShieldTimeRed * numDecimals) / numDecimals + "s";
            //Check if the shield time has run out and deactivate it
            if (ShieldTimeRed <= precision)
            {
                deactivateShield(true);
            }
        }
        else if (BlueShieldUp)
        {
            ShieldTimeBlue -= Time.deltaTime;
            BlueShieldTxt.text = Mathf.Round(ShieldTimeBlue * numDecimals) / numDecimals + "s";
            //Check if the shield time has run out and deactivate it
            if (ShieldTimeBlue <= precision)
            {
                deactivateShield(false);
            }
        }
    }
}
