using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: 
 * Last change date: 
 * Checked by: Peilin Yu
 * Date of check: 2018-04-18
 * Comment: Documentation.
*/

public class Mouse_Drag : MonoBehaviour {

    public float speed;
    public Camera cam;
    public float distance_from_camera;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distance_from_camera = Vector3.Distance(rb.position, cam.transform.position);
    }

    //void FixedUpdate()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    rb.AddForce(movement * speed);
    //}

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = distance_from_camera;
            pos = cam.ScreenToWorldPoint(pos);
            rb.velocity = (pos - rb.position) * 35;
        }

        if(Input.GetMouseButtonUp(0))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
