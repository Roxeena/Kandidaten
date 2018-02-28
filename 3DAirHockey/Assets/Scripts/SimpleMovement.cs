using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    Camera c;
    public float speed = 1;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = Camera.main;
    }

    // When mouse clicks the object and drags it
    void OnMouseDrag()
    {
        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        rb.velocity = ((mousePos - rb.position)*speed);
    }

}