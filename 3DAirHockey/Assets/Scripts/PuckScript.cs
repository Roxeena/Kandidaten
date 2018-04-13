using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour {

    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }
    public PUpScript Shield;
    public Material redMat;
    public Material blueMat;
    public Material puckMat;

    private Rigidbody puck;
    public Collider GoalRed;
    public Collider GoalBlue;
    public Collider Divider;
    private Collider PuckCol;
    private bool didRedStrike = false;

    public AudioManager audioManager;
    public RayMove RedMove;
    public RayMove BlueMove;

    // Use this for initialization
	void Start () {
        puck = GetComponent<Rigidbody>();
        puck.GetComponent<Renderer>().material = puckMat;
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

        if (col.gameObject.CompareTag("Pick Up"))
        {
            col.gameObject.SetActive(false);
            Shield.activateShield(didRedStrike);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.PlayPuckCollision();
        Debug.Log("Collision with: ");
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "golv")
        {
            puck.position = new Vector3(puck.position.x, 0.0f, puck.position.z);
            Debug.Log("Puck locked to floor");
        }

        if (collision.collider.tag == "RedPlayer")
        {
            didRedStrike = true;
            Debug.Log("Puck turn red");
            puck.GetComponent<Renderer>().material = redMat;
        }
        else if(collision.collider.tag == "BluePlayer")
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
        puck.velocity = new Vector3(0, 0, 0);
        puck.GetComponent<Renderer>().material = puckMat;
    }

}
