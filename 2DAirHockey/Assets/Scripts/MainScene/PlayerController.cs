using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public List<PlayerMovement> Players = new List<PlayerMovement>();

    public GameObject AiPlayer;
    public GameObject Player;

    private void Start()
    {
        if (GameValues.IsMultiplayer)
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = true;
            AiPlayer.GetComponent<AiScript>().enabled = false;
        }
        else
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = false;
            AiPlayer.GetComponent<AiScript>().enabled = true;
        }

        if (GameValues.IsMouse)
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = false;
            AiPlayer.GetComponent<MousePlayerMovement>().enabled = true;
            Player.GetComponent<PlayerMovement>().enabled = false;
            Player.GetComponent<MousePlayerMovement>().enabled = true;
        }
        else
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = true;
            AiPlayer.GetComponent<MousePlayerMovement>().enabled = false;
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<MousePlayerMovement>().enabled = false;

        }
    }

   
    
     
    

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            foreach (var player in Players)
            {
                if (player.LockedFingerID == null)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began &&
                        player.PlayerCollider.OverlapPoint(touchWorldPos))
                    {
                        player.LockedFingerID = Input.GetTouch(i).fingerId;
                    }
                }
                else if (player.LockedFingerID == Input.GetTouch(i).fingerId)
                {
                    player.MoveToPosition(touchWorldPos);
                    if (Input.GetTouch(i).phase == TouchPhase.Ended ||
                        Input.GetTouch(i).phase == TouchPhase.Canceled)
                        player.LockedFingerID = null;
                }
            }
        }
    }
}