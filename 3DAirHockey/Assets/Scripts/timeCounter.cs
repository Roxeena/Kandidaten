using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour {

    public bool countDown = true;
    public Text timerTxt; //create a variable to store text in.
    public float gameTime = 1.0f; //the length of a game in minutes
    private float minutesLeft; //the minutes left of the game
    private float secondsLeft; //the seconds left of the game
    private string minutes;
    private string seconds;

    


    // Use this for initialization
    void Start()
    {
        timerTxt.text = "0"; //initialize the timer to "0"'
        minutesLeft = Mathf.Floor(gameTime); //the time left is initalized to the gameTime
        secondsLeft = (gameTime*60 )% 60; //the seconds left are initialized
        minutes = minutesLeft.ToString();
        seconds = seconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        minutesLeft = Mathf.Floor((gameTime * 60 - Time.time)/ 60); //the minuts left are calculated
        secondsLeft = Mathf.Floor((gameTime*60 - Time.time)%60); //the seconds left are calculated

        if(countDown)
        {
            if (minutesLeft < 1)
            {
                minutes = (secondsLeft).ToString();
                seconds = Mathf.Floor((gameTime * 3600 - Time.time * 60) % 60).ToString(); 
                if (Mathf.Floor((gameTime * 3600 - Time.time * 60) % 60) < 10)
                {
                    seconds = string.Concat("0", seconds);
                    timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);
                }
                else
                {
                    timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);
                }
                
            }
            else
            {
                minutes = minutesLeft.ToString();
                seconds = secondsLeft.ToString();
                if (secondsLeft < 10)
                {
                    seconds = string.Concat("0", seconds);
                    timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);
                }
                else
                {
                    timerTxt.text = string.Concat(string.Concat(minutes, ":"),seconds);
                }
                        
            }
            

        }
        else
        {
            timerTxt.text = (Time.time).ToString();
        }


    }
}
