using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour {

    //initializations
    private Camera c;       
    private bool inControl = false;
    private Rigidbody rb;
    private int finger;

    public bool mouseInput = true;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();       
        c = Camera.main;                
    }

    private void FixedUpdate()
    {
        if(mouseInput)
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = c.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                //Debug.Log(vHit.transform.gameObject);
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

                rb.velocity = ((mousePos - rb.position) / Time.deltaTime);

               // rb.position = mousePos;
            }         
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(Input.touchCount-1);            

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
                Touch touchControl = Input.GetTouch(finger);
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

                    rb.velocity = ((touchPos - rb.position) / Time.deltaTime);
                    //rb.position = touchPos;
                    //rb.velocity = touch.deltaPosition/touch.deltaTime;
                }
            }
        }
    }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
