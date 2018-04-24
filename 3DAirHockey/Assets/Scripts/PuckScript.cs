using System.Collections;
using UnityEngine;

/* Author: Malin Ejdbo
 * Last change date: 2018-04-13
 * Checked by: 
 * Date of check: 
 * Comment: 
*/

//Script for the puck object handles collisions, score, picking up power ups, switching the color depending on who struck it last
public class PuckScript : MonoBehaviour {
    // Variables 
    public ScoreScript ScoreScriptInstance;             //Score script that keep track of the scores
    public ShieldScript Shield;                         //Script that handles the shield power up
    public ExpandScript Expand;                         //Script that handles the expand power up
    public ShrinkScript Shrink;                         //Script that handles the shrink power up
    public static bool WasGoal { get; private set; }    //bool to determine if it was goal recently or not
    private Rigidbody puck;                             //The puck object
    public Collider GoalRed, GoalBlue, Divider;         //Colliders the puck should ignore
    private Collider PuckCol;                           //Pucks own collider
    private bool didRedStrike = false;                  //bool to determine who struck last
    public Material RedMat, BlueMat, PuckMat;           //Material the puck changes between depending on who struck it last
    public AudioManager audioManager;                   //To play sound on a collision
    public positionMove RedMove, BlueMove;              //Scripts that determine how the players move, used to reset players after goal

    // Use this for initialization
	void Start () {
        WasGoal = false;
        puck = GetComponent<Rigidbody>();
        PuckCol = puck.GetComponent<Collider>();

        //Ignore these collisions
        Physics.IgnoreCollision(GoalRed, PuckCol);
        Physics.IgnoreCollision(GoalBlue, PuckCol);
        Physics.IgnoreCollision(Divider, PuckCol);
    }

    //When the puck enters a colider that is set as a trigger
    private void OnTriggerEnter(Collider col)
    {
        //this is to not spam, when it is goal this only happen once
        if (!WasGoal)
        {
            //Check if it is goal and which goal it is
            if (col.tag == "BlueGoal")
            {
                //Increment the score and tell game it was goal
                //Reset the players and serve the puck
                ScoreScriptInstance.Increment(ScoreScript.Score.playerBlueScore);
                WasGoal = true;
                RedMove.Serve();
                BlueMove.ResetPosition();
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if(col.tag == "RedGoal")
            {
                //Increment the score and tell game it was goal
                //Reset the players and serve the puck
                ScoreScriptInstance.Increment(ScoreScript.Score.playerRedScore);
                WasGoal = true;
                BlueMove.Serve();
                RedMove.ResetPosition();
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));

            }
        }

        //Check if the puck hit a power up object
        //Depending on who struck the puck last will get the benefits of the power up
        //Shield
        if(col.tag == "ShieldUp")
        {
            Destroy(col.gameObject);
            Shield.activateShield(didRedStrike);
        }
        //Expand
        else if(col.tag == "ExpandOn")
        {
            Destroy(col.gameObject);
            Expand.activateExpand(didRedStrike);
        }
        //Shrink
        else if(col.tag == "ShrinkOn")
        {
            Destroy(col.gameObject);        
            Shrink.activateShrink(didRedStrike);
        }
    }

    //When the puck collides with a collider that is not set as a trigger
    private void OnCollisionEnter(Collision collision)
    {
        //Play audio
        audioManager.PlayPuckCollision();

        // Check which player struck the puck, change color 
        if (collision.collider.tag == "RedPlayer")
        {
            didRedStrike = true;
            puck.GetComponent<Renderer>().material = RedMat;
        }
        else if (collision.collider.tag == "BluePlayer")
        {
            didRedStrike = false;
            puck.GetComponent<Renderer>().material = BlueMat;
        }

        // Check if the puck collided with a shield, if so the decrements its lives
        if (collision.collider.tag == "RedShield")
            Shield.decrement(true);
        else if (collision.collider.tag == "BlueShield")
            Shield.decrement(false);

    }

    //Reset the puck after a small delay, reset material on puck
    private IEnumerator ResetPuck(bool didPlayerRedScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        puck.velocity = puck.position = new Vector3(0, 0, 0);
        puck.GetComponent<Renderer>().material = PuckMat;

        //Depending on who made a goal, the other person gets to serve
        if (didPlayerRedScore)
            puck.position = new Vector3(0, 2, -1);
        else
            puck.position = new Vector3(0, 2, 1);
    }

    //Drop the puck from the center, reset puck material
    public void CenterPuck()
    {
        puck.position = new Vector3(0, 2, 0);
        puck.velocity = Vector3.zero;
        puck.GetComponent<Renderer>().material = PuckMat;
    }

	/* //Debug code
     // Update is called once per frame
	void Update () {

        //debugcode
        if(puck.velocity.y > 0)
        {
           // Debug.Log(puck.velocity.y);
        }		
	}
    */
}
