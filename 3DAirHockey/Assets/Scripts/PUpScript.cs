using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUpScript : MonoBehaviour {
    public Collider ShieldRedGoal;
    public Collider ShieldBlueGoal;
    public int PUpTime = 10;

    private float ShieldTimeRed;
    private float ShieldTimeBlue;
    private bool RedShieldUp;
    private bool BlueShieldUp;

    //Use this for initialization
    void Start()
    {
        ShieldTimeRed = ShieldTimeBlue = PUpTime;
        
    }

    public void activateShield(bool didRedPickUp)
    {
        if (didRedPickUp)
        {
            ShieldRedGoal.gameObject.SetActive(true);
            RedShieldUp = true;
        }
        else
        {
            ShieldBlueGoal.gameObject.SetActive(true);
            BlueShieldUp = true;
        }
    }

    public void deactivateShield(bool Red)
    {
        if(Red)
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

    //Update is called once per frame
    void Update()
    {
        if(RedShieldUp)
        {
            ShieldTimeRed -= Time.deltaTime;
            //Debug.Log(ShieldTimeRed);
            if(ShieldTimeRed <= 0.01)
            {
                deactivateShield(true);
            }
        }
        else if (BlueShieldUp)
        {
            ShieldTimeBlue -= Time.deltaTime;
            //Debug.Log(ShieldTimeBlue);
            if (ShieldTimeBlue <= 0.01)
            {
                deactivateShield(false);
            }
        }

        
    }

}
