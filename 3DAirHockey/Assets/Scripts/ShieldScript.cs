using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour {
    public Collider ShieldRedGoal, ShieldBlueGoal;
    public uint ShieldTime = 10;
    public Text ShieldRedTxt, ShieldBlueTxt;
    public UiManager uiManager;
    public uint Lives = 3;

    private uint LivesRed, LivesBlue;
    private float ShieldTimeRed, ShieldTimeBlue;
    private bool RedShieldUp, BlueShieldUp;

    //Use this for initialization
    void Start()
    {
        ShieldTimeRed = ShieldTimeBlue = ShieldTime;
        LivesRed = LivesBlue = Lives;
    }

    public void activateShield(bool didRedPickUp)
    {
        if (didRedPickUp)
        {
            ShieldRedGoal.gameObject.SetActive(true);
            RedShieldUp = true;
            ShieldRedTxt.gameObject.SetActive(true);
        }
        else
        {
            ShieldBlueGoal.gameObject.SetActive(true);
            BlueShieldUp = true;
            ShieldBlueTxt.gameObject.SetActive(true);
        }
    }

    public void deactivateShield(bool Red)
    {
        if(Red)
        {
            ShieldRedGoal.gameObject.SetActive(false);
            RedShieldUp = false;
            ShieldRedTxt.gameObject.SetActive(false);
            LivesRed = Lives;
        }
            
        else
        {
            ShieldBlueGoal.gameObject.SetActive(false);
            BlueShieldUp = false;
            ShieldBlueTxt.gameObject.SetActive(false);
            LivesBlue = Lives;
        }     
    }

    public void decrement(bool Red)
    {
        if(Red)
            --LivesRed;
        else
            --LivesBlue;
        if (LivesRed == 0 || LivesBlue == 0)
            deactivateShield(Red);
    }
    
    //Update is called once per frame
    void Update()
    {
        if(RedShieldUp)
        {
            ShieldTimeRed -= Time.deltaTime;
            ShieldRedTxt.text = Mathf.Round(ShieldTimeRed * 1f) / 1f + "s"; 
            if(ShieldTimeRed <= 0.01)
            {
                deactivateShield(true);
            }
        }
        else if (BlueShieldUp)
        {
            ShieldTimeBlue -= Time.deltaTime;
            ShieldBlueTxt.text = Mathf.Round(ShieldTimeBlue * 1f) / 1f   + "s";
            if (ShieldTimeBlue <= 0.01)
            {
                deactivateShield(false);
            }
        }
    }

}
