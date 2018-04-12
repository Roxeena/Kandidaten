 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour {

    public ScoreScript ScoreScriptInstance;
    public ShieldScript Shield;
    public Material redMat, blueMat, puckMat;
    public static bool WasGoal { get; private set; }

    private Rigidbody puck;
    public Collider GoalRed, GoalBlue, Divider, PuckCol;
    private bool didRedStrike = false;

    public AudioManager audioManager;
    public positionMove RedMove, BlueMove;

    // Use this for initialization
	void Start () {
        puck = GetComponent<Rigidbody>();
        PuckCol = puck.GetComponent<Collider>();
        WasGoal = false;
        Physics.IgnoreCollision(GoalRed, PuckCol);
        Physics.IgnoreCollision(GoalBlue, PuckCol);
        Physics.IgnoreCollision(Divider, PuckCol);
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if (!WasGoal)
        {
            if (col.tag == "BlueGoal")
            {
                Debug.Log("Blue Scored");
                ScoreScriptInstance.Increment(ScoreScript.Score.playerBlueScore);
                WasGoal = true;
                audioManager.PlayGoal();
                RedMove.Serve();
                BlueMove.ResetPosition();
                StartCoroutine(ResetPuck(false));
            }
            else if(col.tag == "RedGoal")
            {
                Debug.Log("Red Scored");
                ScoreScriptInstance.Increment(ScoreScript.Score.playerRedScore);
                WasGoal = true;
                audioManager.PlayGoal();
                BlueMove.Serve();
                RedMove.ResetPosition();
                StartCoroutine(ResetPuck(true));
            }
        }

        if (col.gameObject.CompareTag("Shield Up"))
        {
            col.gameObject.SetActive(false);
            Shield.activateShield(didRedStrike);
        }
        
        if(col.gameObject.CompareTag("Shield Red"))
        {
            Shield.decrement(true);
        }
        else if(col.gameObject.CompareTag("Shield Blue"))
        {
            Shield.decrement(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.PlayPuckCollision();

        if (collision.collider.tag == "RedPlayer")
        {
            didRedStrike = true;
            Debug.Log("Puck turn red");
            puck.GetComponent<Renderer>().material = redMat;
        }
        else if (collision.collider.tag == "BluePlayer")
        {
            didRedStrike = false;
            puck.GetComponent<Renderer>().material = blueMat;
        }
    }

    private IEnumerator ResetPuck(bool didPlayerRedScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        puck.velocity = new Vector3(0, 0, 0);
        puck.GetComponent<Renderer>().material = puckMat;

        if (didPlayerRedScore)
            puck.position = new Vector3(0, 2, -1);
        else
            puck.position = new Vector3(0, 2, 1);
    }

    public void CenterPuck()
    {
        puck.position = new Vector3(0, 2, 0);
        puck.velocity = Vector3.zero;
        puck.transform.Rotate(Vector3.zero);
        puck.GetComponent<Renderer>().material = puckMat;
    }

	// Update is called once per frame
	void Update () {

        //debugcode
        if(puck.velocity.y > 0)
        {
           // Debug.Log(puck.velocity.y);
        }		
	}
}
