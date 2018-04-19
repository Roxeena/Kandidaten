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
    public GameObject redWaiting;

    public GameObject player2WinTxt;
    public GameObject player2LoseTxt;
    public GameObject blueRestart;
    public GameObject blueWaiting;

    public GameObject tieTxt;

    [Header("Other")]
    public AudioManager audioManager;
    public Restart restartScript;
    public ScoreScript scoreScript;
    

    public PuckScript puckScript;
    public positionMove RedMove;
    public positionMove BlueMove;


    public void ShowRestartCanvas()
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (scoreScript.playerScore < scoreScript.aiScore)
        {
            audioManager.PlayLostGame();
            player1WinTxt.SetActive(false);
            player1LoseTxt.SetActive(true);
            player2WinTxt.SetActive(true);
            player2LoseTxt.SetActive(false);
            tieTxt.SetActive(false);
        }
        else if (scoreScript.aiScore < scoreScript.playerScore)
        {
            audioManager.PlayWonGame();
            player1WinTxt.SetActive(true);
            player1LoseTxt.SetActive(false);
            player2WinTxt.SetActive(false);
            player2LoseTxt.SetActive(true);
            tieTxt.SetActive(false);
        }
        else //it was a tie
        {
            audioManager.PlayLostGame();
            player1WinTxt.SetActive(false);
            player1LoseTxt.SetActive(false);
            player2WinTxt.SetActive(false);
            player2LoseTxt.SetActive(false);

            tieTxt.SetActive(true);
        }
    }

    public int redReady = 0;
    public int blueReady = 0;

    public void RestartGame()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "RestartBtnRed")
        {
            print("Red Ready");
            redReady = 1;
            redRestart.SetActive(false);
            redWaiting.SetActive(true);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "RestartBtnBlue")
        {
            print("Blue Ready");
            blueReady = 1;
            blueRestart.SetActive(false);
            blueWaiting.SetActive(true);
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

            redRestart.SetActive(true);
            redWaiting.SetActive(false);
            blueRestart.SetActive(true);
            blueWaiting.SetActive(false);

            redReady = 0;
            blueReady = 0;

            //reloads the scene, resets score and time.
            restartScript.Reload_scene();
        }
    }
}
