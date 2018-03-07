using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour {

   
    public Text timerTxt; //create a variable to store text in.
    public bool countDown = true;
    public bool repeat = true;
    public float gameTime = 1.0f; //the length of a game in minutes
    private float minutesLeft; //the minutes left of the game
    private float secondsLeft; //the seconds left of the game
    private string minutes;
    private string seconds;
    private float miliSeconds = 0;

    


    // Use this for initialization
    void Start()
    {
        timerTxt.text = "0"; //initialize the timer to "0"'
        if(countDown)
        {
            minutesLeft = Mathf.Floor(gameTime); //the time left is initalized to the gameTime
            secondsLeft = (gameTime*60.0f )% 60.0f; //the seconds left are initialized
        }
        else
        {
            minutesLeft = 0; 
            secondsLeft = 0; 
        }
        
        minutes = minutesLeft.ToString();
        seconds = secondsLeft.ToString();
        timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            if (minutesLeft < 0 || (minutesLeft == 0 && secondsLeft == 0)) //time is out
            {
                secondsLeft = 0;
                minutesLeft = 0;
                timerTxt.text = "00:00";
                if(repeat)
                {
                    minutesLeft = Mathf.Floor(gameTime); //the time lis reest to the gameTime
                    secondsLeft = (gameTime * 60.0f) % 60.0f; //the seconds are reset
                }
               

                //Send an event/trigger to the game to notify about the end of the round

            }
            else
            {
                if (secondsLeft <= 0) //1 minute has passed
                {
                    secondsLeft = 60.0f;//reset seconds
                    minutesLeft = minutesLeft - 1;//decrement minutes
                }

                secondsLeft = (secondsLeft - Time.deltaTime) % 60.0f; //the seconds left are calculated

                if (minutesLeft < 1) //change to ss:ms
                {
                    miliSeconds = (secondsLeft * 60 - Time.deltaTime * 60) % 60; // claculate milisecodns
                    minutes = Mathf.Floor(secondsLeft).ToString();
                    seconds = Mathf.Floor(miliSeconds).ToString();
                    if (secondsLeft < 10) //add "0" before minutes if secondsLeft < 10
                    {
                        minutes = string.Concat("0", minutes);
                    }
                    if (miliSeconds < 10) //add "0" before seconds if miliseconds  < 10
                    {
                        seconds = string.Concat("0", seconds);                       
                    }           
                  
                     timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);  //save the output to the text
                }
                else // mm:ss
                {
                    minutes = Mathf.Floor(minutesLeft).ToString();
                    seconds = Mathf.Floor(secondsLeft).ToString();
                    if (minutesLeft < 10) //add "0" beofre minutes
                    {
                        minutes = string.Concat("0", minutes);
                    }
                    if (secondsLeft < 10) // add "0" before seconds
                    {
                        seconds = string.Concat("0", seconds);
                    }    
                    
                     timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);     //save the output to the text
                }
            }
        }
        else //count up
        {
            if ( repeat && (minutesLeft*60 + secondsLeft) > gameTime*60) //time is out
            {
                secondsLeft = 0;
                minutesLeft = 0;
                timerTxt.text = "00:00";
                //Send an event/trigger to the game to notify about the end of the round

            }
            else
            {                
                secondsLeft = (secondsLeft + Time.deltaTime); //the seconds left are calculated

                if (secondsLeft >= 60) //1 minute has passed
                {
                    secondsLeft = 0.0f;//reset seconds
                    minutesLeft = minutesLeft + 1.0f;//increment minutes                    
                }
                if (minutesLeft < 1) //change to ss:ms
                {
                    miliSeconds = (secondsLeft * 60 + Time.deltaTime * 60) % 60; // claculate milisecodns
                    minutes = Mathf.Floor(secondsLeft).ToString();
                    seconds = Mathf.Floor(miliSeconds).ToString();
                    if (secondsLeft < 10) //add "0" before minutes if secondsLeft < 10
                    {
                        minutes = string.Concat("0", minutes);
                    }
                    if (miliSeconds < 10) //add "0" before seconds if miliseconds  < 10
                    {
                        seconds = string.Concat("0", seconds);
                    }

                    timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);  //save the output to the text
                }
                else // mm:ss
                {
                    minutes = Mathf.Floor(minutesLeft).ToString();
                    seconds = Mathf.Floor(secondsLeft).ToString();
                    if (minutesLeft < 10) //add "0" beofre minutes
                    {
                        minutes = string.Concat("0", minutes);
                    }
                    if (secondsLeft < 10) // add "0" before seconds
                    {
                        seconds = string.Concat("0", seconds);
                    }

                    timerTxt.text = string.Concat(string.Concat(minutes, ":"), seconds);     //save the output to the text
                }
            }            
        }
    }
}
