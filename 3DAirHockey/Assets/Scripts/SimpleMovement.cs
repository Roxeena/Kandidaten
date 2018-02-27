using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    bool wasJustClicked = true;
    bool canMove;
    Vector3 playerSize;
    Vector2 p = new Vector2();
    Camera c;

    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        playerSize = GetComponent<MeshRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody>();
        c = Camera.main;

    }

    //private void OnMouseDrag()
    //{
    //    rb.MovePosition((Input.mousePosition));
    //    Debug.Log(c.ScreenToWorldPoint(Input.mousePosition));
    //    Debug.Log((Input.mousePosition));
    //}

    void OnMouseDrag()
    {
        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
        //transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        Vector3 temp = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.position = new Vector3(temp.x, 0.0191f, temp.z);
       // rb.velocity

    }

    // Update is called once per frame
    void Update()
    {
        //    if (Input.GetMouseButton(0))
        //    {

        //      //  Debug.Log(Event.current);
        //        //Debug.Log("mousepos:");
        //        //Debug.Log(Input.mousePosition);
        //        //Debug.Log("worldpos:");
        //        //Debug.Log(c);

        //       // p.x = Event.current.mousePosition.x;
        //        //p.y = Camera.main.pixelHeight - Event.current.mousePosition.y;

        //        Vector3 mousePos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane) );

        //        //Debug.Log("mousepos:");
        //        //Debug.Log(mousePos);

        //        if (wasJustClicked)
        //        {
        //            //Debug.Log("input.mousepos:");
        //            //Debug.Log(Input.mousePosition);
        //            //Debug.Log("worldpos:");
        //            //Debug.Log(c.ScreenToWorldPoint(Input.mousePosition));



        //            wasJustClicked = false;

        //            if ((mousePos.x >= transform.position.x && mousePos.x < transform.position.x + playerSize.x ||
        //            mousePos.x <= transform.position.x && mousePos.x > transform.position.x - playerSize.x) &&
        //            (mousePos.y >= transform.position.z && mousePos.y < transform.position.z + playerSize.z ||
        //            mousePos.y <= transform.position.z && mousePos.y > transform.position.z - playerSize.z))
        //            {
        //                canMove = true;
        //                Debug.Log(canMove);
        //            }
        //            else
        //            {
        //                canMove = false;
        //                Debug.Log(canMove);
        //            }
        //        }

        //        if (canMove)
        //        {

        //            rb.MovePosition(mousePos);
        //        }
        //    }
        //    else
        //    {
        //        wasJustClicked = true;
        //    }
        //}
    }
}