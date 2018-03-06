using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    Camera c;
    public float speed = 1;
    Rigidbody rb;

    public bool mouseInput = true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = Camera.main;
    }

    // When mouse clicks the object and drags it
    void OnMouseDrag()
    {
        if(mouseInput)
        {
        float distance_to_screen = c.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        rb .velocity = ((mousePos - rb.position)*speed);        
        }
       
    }

    private void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                Debug.Log("TAP !");

                if (hit.collider != null)
                {

                    rb.velocity = new Vector3(Input.GetTouch(i).deltaPosition.x, Input.GetTouch(i).deltaPosition.y);

                }
            }
            ++i;
        }

    }
}

