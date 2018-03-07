using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public enum Score
    {
        playerRedScore, playerBlueScore
    }

    public Text playerRedTxt, playerBlueTxt;

    public UiManager uiManager;

    public int MaxScore;

    private int aiScore, playerScore;

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
