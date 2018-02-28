using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public enum Score
    {
        playerRedScore, playerBlueScore
    }

    public Text playerRedTxt, playerBlueTxt;

    //public UiManager uiManager;

    public int MaxScore;

    #region Scores
    private int playerRedScore, playerBlueScore;

    private int PlayerRedScore
    {
        get { return playerRedScore; }
        set
        {
            playerRedScore = value;
            /*if (value == MaxScore)
                uiManager.ShowRestartCanvas(true);*/
        }
    }

    private int PlayerBlueScore
    {
        get { return playerBlueScore; }
        set
        {
            playerBlueScore = value;
            /*if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);*/
        }
    }
    #endregion

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
