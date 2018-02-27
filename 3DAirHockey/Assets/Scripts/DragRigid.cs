using UnityEngine;
using System.Collections;

public class DragRigid : MonoBehaviour
{
    private bool isTouched = false;
    Rigidbody Player;

    private void Start()
    {
        Player = GetComponent<Rigidbody>();
            
    }

    private void OnMouseDrag()
    {
        Player.MovePosition(new Vector3(Input.mousePosition.y, 0, Input.mousePosition.x));
    }

    private void Update()
    {
        
    }

}