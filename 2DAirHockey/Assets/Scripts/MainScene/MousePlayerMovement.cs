using UnityEngine;

public class MousePlayerMovement : MonoBehaviour
{

    bool wasJustClicked = true;
    bool canMove;

    Rigidbody2D rb;
    Vector2 startingPosition;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

    Collider2D playerCollider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        playerCollider = GetComponent<Collider2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {            
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;           
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

            if (wasJustClicked)
            {
                Debug.Log("wasJustClicked");
                wasJustClicked = false;

                if (playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }

            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                  playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                  playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}