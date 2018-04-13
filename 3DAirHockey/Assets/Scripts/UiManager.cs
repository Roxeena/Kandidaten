using UnityEngine;
using UnityEngine.EventSystems;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-04-13
 * Comment: Dokumentation.
*/

public class UiManager : MonoBehaviour
{

    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject player1WinTxt;
    public GameObject player1LoseTxt;
    public GameObject redRestart;

    public GameObject player2WinTxt;
    public GameObject player2LoseTxt;
    public GameObject blueRestart;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;
    

    public PuckScript puckScript;
    public positionMove RedMove;
    public positionMove BlueMove;


    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAiWin)
        {
            audioManager.PlayLostGame();
            player1WinTxt.SetActive(false);
            player1LoseTxt.SetActive(true);
            player2WinTxt.SetActive(true);
            player2LoseTxt.SetActive(false);
        }
        else
        {
            audioManager.PlayWonGame();
            player1WinTxt.SetActive(true);
            player1LoseTxt.SetActive(false);
            player2WinTxt.SetActive(false);
            player2LoseTxt.SetActive(true);
        }
    }

    public int redReady = 0;
    public int blueReady = 0;

    public void RestartGame()
    {
        GameObject redRestart = GameObject.Find("RestartBtnRed");

        if (EventSystem.current.currentSelectedGameObject.name == "RestartBtnRed")
        {
            if (redReady == 1)
            {
                redReady = 0;
            }
            else
            {
                print("Red Ready");
                redReady = 1;
            }
            
        }

        if (EventSystem.current.currentSelectedGameObject.name == "RestartBtnBlue")
        {
            if (blueReady == 1)
            {
                blueReady = 0;
            }
            else
            {
                print("Blue Ready");
                blueReady = 1;
            }
        }

        if (redReady == 1 && blueReady == 1)
        {
            Time.timeScale = 1;

            CanvasGame.SetActive(true);
            CanvasRestart.SetActive(false);
            RedMove.ResetPosition();
            BlueMove.ResetPosition();
            scoreScript.ResetScores();
            puckScript.CenterPuck();

            redReady = 0;
            blueReady = 0;
        }
    }
}
