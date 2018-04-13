using UnityEngine;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-04-13
 * Comment: The collision is not working very well at all times. Dokumentation
*/

//Does not yet support multitouch!
public class positionMove : MonoBehaviour {


    //initializations
    private Camera c;       
    private bool inControl = false;
    private Rigidbody rb;
    private int finger;
    private bool aboutToCollide;
    private float distanceToCollision;
    private Collider col;

    public Collider Puck;     //used to add force to puck when it's hit
    public Collider Goal;    //used to ingorre this collider
    public bool mouseInput = true;          //controlled form the startmenu
    public bool mouseInputNoMenu = true;    //use this to control mousinput without using the startmenu

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();       
        c = Camera.main;
        col = GetComponent<Collider>();
        Physics.IgnoreCollision(Goal, col);//behövs kanske inte?


        if (GameValues.IsMouse)
        {
            mouseInput = true;

        }
        else if (!GameValues.IsMouse)
        {
            mouseInput = false;
        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;
        
        //move to FixedUpdate?
        if (mouseInput || mouseInputNoMenu)
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = c.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
               // Debug.Log(vHit.transform.gameObject);
            }

            //If we press on (touch) the gameObject we are inControl of it
            if (vHit.transform.gameObject != null && (Input.GetMouseButtonDown(0) && vHit.transform.gameObject == rb.gameObject))
            {
                inControl = true;
            }

            //if we release the button we are no longer inControl of the gameObject
            if (Input.GetMouseButtonUp(0))
            {
                inControl = false;
            }

            //While inCOntrol the gameOmbject will follow the mouse 
            if (inControl)
            {
                float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
                Vector3 mousePos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

                Vector3 direction = mousePos - rb.position;
                float distance = direction.magnitude;

                //have a trigger collider move in front of the striker to test on collsion? sweep or spherecast!

                RaycastHit hit;
                if (rb.SweepTest(direction, out hit, distance))
                {
                    aboutToCollide = true;
                    distanceToCollision = hit.distance;
                }
                else
                {
                    aboutToCollide = false;
                }
                if (aboutToCollide)
                {
                    //Debug.Log("Collsions iminent !");
                    if (hit.collider.CompareTag("Puck"))
                    {
                        if(Time.deltaTime != 0)
                        {
                        Vector3 StrikeVelocity = direction / Time.deltaTime;
                        Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                        Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                        Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                        hit.rigidbody.AddForce(ForceDirection);
                        }
                        
                      //  Debug.Log(ForceDirection);
                    }
                    float f = distanceToCollision - 0.1f;
                    rb.position = rb.position + direction.normalized * f;
                }
                else
                {
                    //Debug.Log("no Collsions!");
                    rb.position = mousePos;
                }
            }
        }


        //supports multitouch! preblem with touch ID, include constraint of which half of spelplanen finger is on when testing the touches?
        if (Input.touchCount > 0 && !mouseInputNoMenu)
        {
            Touch touch = Input.GetTouch(Input.touchCount - 1);

            RaycastHit vHit = new RaycastHit();
            Ray vRay = c.ScreenPointToRay(touch.position);

            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                //Debug.Log(vHit.transform.gameObject);
            }

            //If we touch the gameObject we are inControl of it
            if (vHit.transform.gameObject != null && (touch.phase == TouchPhase.Began && vHit.transform.gameObject == rb.gameObject))
            {
                inControl = true;
                finger = touch.fingerId;
            }



            //While inCOntrol the gameOmbject will follow the touch input
            if (inControl)
            {
                Touch touchControl = touch;
                bool found = false;
                foreach (Touch t in Input.touches) {
                    if (t.fingerId == finger)
                    {
                        touchControl = t;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    //if we lift the finger we are no longer inControl of the gameObject
                    if (touchControl.phase == TouchPhase.Ended)
                    {
                        inControl = false;
                        //OBS! if one player lets go of their stricker niether player is inControl of their striker.
                    }
                    else
                    {
                        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
                        Vector3 touchPos = c.ScreenToWorldPoint(new Vector3(touchControl.position.x, touchControl.position.y, distance_to_screen));

                        Vector3 direction = touchPos - rb.position;
                        float distance = direction.magnitude;

                        RaycastHit hit;
                        if (rb.SweepTest(direction, out hit, distance))
                        {
                            aboutToCollide = true;
                            distanceToCollision = hit.distance;
                        }
                        else
                        {
                            aboutToCollide = false;
                        }
                        if (aboutToCollide)
                        {
                            //Debug.Log("Collsions iminent !");
                            if (hit.collider.CompareTag("Puck"))
                            {
                                Vector3 StrikeVelocity = direction / Time.deltaTime;
                                Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                                Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                                Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                                hit.rigidbody.AddForce(ForceDirection);
                                // Debug.Log(ForceDirection);
                            }
                            float f = distanceToCollision - 0.1f;
                            rb.position = rb.position + direction.normalized * f;
                        }
                        else
                        {
                            //Debug.Log("no Collsions!");
                            rb.position = touchPos;
                        }
                    }
                }
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("colliding through something!!!");
    //    Debug.Log(collision.collider);
        
    //}

    public void ResetPosition()
    {

        if (rb.tag == "RedPlayer")
        {
            rb.position = new Vector3(0, 0.1f, 2.5f);
            rb.velocity = Vector3.zero;
            Debug.Log("Red Reset");
            inControl = false;
        }

        else if (rb.tag == "BluePlayer")
        {
            rb.position = new Vector3(0, 0.1f, -2.5f);
            rb.velocity = Vector3.zero;
            Debug.Log("Blue Reset");
            inControl = false;
        }
    }

    public void Serve()
    {

        if (rb.tag == "RedPlayer")
        {
            rb.position = new Vector3(0, 0.1f, 3.5f);
            rb.velocity = Vector3.zero;
            Debug.Log("Red Serve");
            inControl = false;
        }

        else if (rb.tag == "BluePlayer")
        {
            rb.position = new Vector3(0, 0.1f, -3.5f);
            rb.velocity = Vector3.zero;
            Debug.Log("Blue Serve");
            inControl = false;
        }
    }
}
