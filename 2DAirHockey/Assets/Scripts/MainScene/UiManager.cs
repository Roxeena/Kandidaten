using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    #region Singleton
    public static UiManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LooseTxt;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;

    public List<IResettable> ResetableGameObjects = new List<IResettable>();

    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAiWin)
        {
            audioManager.PlayLostGame();
            WinTxt.SetActive(false);
            LooseTxt.SetActive(true);
        }
        else
        {
            audioManager.PlayWonGame();
            WinTxt.SetActive(true);
            LooseTxt.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScores();

        foreach (var obj in ResetableGameObjects)
            obj.ResetPosition();
    }

    public void ShowMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }
}