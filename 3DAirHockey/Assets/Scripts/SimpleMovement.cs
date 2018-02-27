using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    bool wasJustClicked = true;
    bool canMove;
    Vector3 playerSize;
    Vector2 p = new Vector2();
    Camera c;
    public float speed = 1;
    Touch finger;

    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        playerSize = GetComponent<MeshRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody>();
        c = Camera.main;

    }

    void OnMouseDrag()
    {
        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        rb.velocity = ((mousePos - rb.position)*speed);
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}