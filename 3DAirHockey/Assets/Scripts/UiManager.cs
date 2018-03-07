using UnityEngine;

public class UiManager : MonoBehaviour
{

    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject player1WinTxt;
    public GameObject player1LoseTxt;

    public GameObject player2WinTxt;
    public GameObject player2LoseTxt;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;

    public PuckScript puckScript;
    public SimpleMovement simpleMovement;

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

    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScores();
        puckScript.CenterPuck();
        simpleMovement.ResetPosition();
    }
}
