using UnityEngine;
using UnityEngine.UI;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-04-13
 * Comment: Dokumentation.
*/

public class ScoreScript : MonoBehaviour {

	public enum Score
    {
        playerRedScore, playerBlueScore
    }

    public Text playerRedTxt, playerBlueTxt;

    public UiManager uiManager;

    public int MaxScore;

    public int aiScore, playerScore = 0;

    private int playerBlueScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true);
        }
    }

    private int playerRedScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);
        }
    }



  

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.playerRedScore)
            playerRedTxt.text = (++playerRedScore).ToString();
        else
            playerBlueTxt.text = (++playerBlueScore).ToString();
    }

    public void ResetScores()
    {
        playerRedScore = playerBlueScore = 0;
        playerRedTxt.text = playerBlueTxt.text = "0";
    }
}
