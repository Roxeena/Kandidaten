using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    public PuckScript puckScript; //allows us to call public functions of the puck script

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
                //  Debug.Log(vHit.transform.gameObject);
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

                RaycastHit hit;
                if (rb.SweepTest(direction, out hit, distance) && !hit.collider.isTrigger) //don't collide with trigger colliders!
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
                    if (hit.collider.CompareTag("Puck")) //we hit the puck!
                    {
                        puckScript.puckHit(col);//calls the pucks script for hitting strikers
                        //callculate force to be transfered to the puck
                        if (Time.deltaTime != 0) //don't divide with zero.
                        {
                            Vector3 StrikeVelocity = direction / Time.deltaTime;
                            Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                            Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                            Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                            //hit.rigidbody.AddForce(ForceDirection);
                            hit.rigidbody.AddForceAtPosition(ForceDirection, hit.point);//add force to puck
                        }
                    }
                    float f = distanceToCollision - 0.1f;//we want' to stop before colliding
                    rb.position = rb.position + direction.normalized * f;
                    //calculate how the puck will "glide" against obstructing surfaces
                    //project component parallel with the hit surface
                    Vector3 normalDirection = Vector3.Dot(direction, -hit.normal) * (-hit.normal);
                    Vector3 tangentDirection = direction - normalDirection;
                    //see if we hit something while gliding
                    RaycastHit hit2;
                    if (rb.SweepTest(tangentDirection, out hit2, tangentDirection.magnitude) && !hit2.collider.isTrigger)
                    {
                        if (hit2.collider.CompareTag("Puck"))//did we  hit the puck?
                        {
                            puckScript.puckHit(col);//calls the pucks script for hitting strikers
                            if (Time.deltaTime != 0)
                            {
                                Vector3 StrikeVelocity = tangentDirection / Time.deltaTime;
                                Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                                Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                                Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                                hit2.rigidbody.AddForceAtPosition(ForceDirection, hit2.point);
                            }
                        }
                        distanceToCollision = hit2.distance;
                        float k = distanceToCollision - 0.1f;//we want' to stop before colliding
                        rb.position = rb.position + tangentDirection.normalized * k;
                    }
                    else
                    {
                        rb.position = rb.position + tangentDirection;//we move!
                    }

                }
                else
                {
                    rb.position = mousePos;//we didn't hit anything on the way so we move the striker to the mousepointer.
                }
            }
        }


        //supports multitouch!
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
                foreach (Touch t in Input.touches)
                {
                    if (t.fingerId == finger)//is it the same finger that began touching the striker?
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
                    }
                    else
                    {
                        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
                        Vector3 touchPos = c.ScreenToWorldPoint(new Vector3(touchControl.position.x, touchControl.position.y, distance_to_screen));

                        Vector3 direction = touchPos - rb.position;
                        float distance = direction.magnitude;

                        RaycastHit hit;
                        if (rb.SweepTest(direction, out hit, distance) && !hit.collider.isTrigger)//do we collide with anything when we try to move? ignore trigger colliders
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
                            if (hit.collider.CompareTag("Puck"))//we hit the puck!
                            {
                                puckScript.puckHit(col);//calls the pucks script for hitting strikers
                                if (Time.deltaTime != 0)
                                {
                                    //callculate the froce to betransfered to the puck
                                    Vector3 StrikeVelocity = direction / Time.deltaTime;
                                    Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                                    Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                                    Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                                    hit.rigidbody.AddForce(ForceDirection);//add force to the puck
                                }
                            }
                            float f = distanceToCollision - 0.1f;
                            rb.position = rb.position + direction.normalized * f;//stop before enteriong the obstructing object
                            //callculate how the striker will "glide" against obstructing surfaces
                            Vector3 normalDirection = Vector3.Dot(direction, -hit.normal) * (-hit.normal);
                            Vector3 tangentDirection = direction - normalDirection;

                            RaycastHit hit2;
                            if (rb.SweepTest(tangentDirection, out hit2, tangentDirection.magnitude) && !hit2.collider.isTrigger)
                            {
                                if (hit2.collider.CompareTag("Puck"))
                                {
                                    puckScript.puckHit(col);//calls the pucks script for hitting strikers
                                    if (Time.deltaTime != 0)
                                    {
                                        Vector3 StrikeVelocity = tangentDirection / Time.deltaTime;
                                        Vector3 StrikeAcc = StrikeVelocity / Time.deltaTime;
                                        Vector3 StrikeForce = StrikeAcc * rb.mass; //for good Force (massa puck)>(massa klubba), why? Pontus Doesn't know
                                        Vector3 ForceDirection = new Vector3(StrikeForce.x, 0, StrikeForce.z);
                                        //hit2.rigidbody.AddForce(ForceDirection);
                                        hit2.rigidbody.AddForceAtPosition(ForceDirection, hit2.point);
                                    }
                                }
                                distanceToCollision = hit2.distance;
                                float k = distanceToCollision - 0.1f;
                                rb.position = rb.position + tangentDirection.normalized * k;
                            }
                            else
                            {
                                rb.position = rb.position + tangentDirection;
                            }
                        }
                        else
                        {
                            rb.position = touchPos;//we didn't hit anything so we move the striker to the finger
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
            inControl = false;
        }

        else if (rb.tag == "BluePlayer")
        {
            rb.position = new Vector3(0, 0.1f, -2.5f);
            rb.velocity = Vector3.zero;
            inControl = false;
        }
    }

    public void Serve()
    {

        if (rb.tag == "RedPlayer")
        {
            rb.position = new Vector3(0, 0.1f, 3.5f);
            rb.velocity = Vector3.zero;
            inControl = false;
        }

        else if (rb.tag == "BluePlayer")
        {
            rb.position = new Vector3(0, 0.1f, -3.5f);
            rb.velocity = Vector3.zero;
            inControl = false;
        }
    }
}
