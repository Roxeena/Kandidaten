using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour {

    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }

    private Rigidbody puck;
    public Collider GoalRed;
    public Collider GoalBlue;
    public Collider Divider;
    private Collider PuckCol;

    public AudioManager audioManager;
    public RayMove RedMove;
    public RayMove BlueMove;

    // Use this for initialization
	void Start () {
        puck = GetComponent<Rigidbody>();
        PuckCol = puck.GetComponent<Collider>();
        WasGoal = false;
        Physics.IgnoreCollision(GoalRed, PuckCol);
        Physics.IgnoreCollision(GoalBlue, PuckCol);
        Physics.IgnoreCollision(Divider, PuckCol);
    }

    private void OnTriggerEnter(Collider goal)
    {
        Debug.Log(goal.tag);
        if (!WasGoal)
        {
            Debug.Log("Goal!");
            if (goal.tag == "BlueGoal")
            {
                Debug.Log("Blue");
                ScoreScriptInstance.Increment(ScoreScript.Score.playerBlueScore);
                WasGoal = true;
                RedMove.Serve();
                BlueMove.ResetPosition();
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if(goal.tag == "RedGoal")
            {
                Debug.Log("Red");
                ScoreScriptInstance.Increment(ScoreScript.Score.playerRedScore);
                WasGoal = true;
                BlueMove.Serve();
                RedMove.ResetPosition();
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.PlayPuckCollision();
    }

    private IEnumerator ResetPuck(bool didPlayerRedScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        puck.velocity = puck.position = new Vector3(0, 0, 0);

        if (didPlayerRedScore)
            puck.position = new Vector3(0, 2, -1);
        else
            puck.position = new Vector3(0, 2, 1);
    }

    public void CenterPuck()
    {
        puck.position = new Vector3(0, 2, 0);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
