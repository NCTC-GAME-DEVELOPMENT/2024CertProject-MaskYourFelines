using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerManager : Pawn
{
    public int playerNum = 1; 
    public float moveSpeed = 10f;
    public float jumpSpeed = 200f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Left Stick Mapping 
    // A/D on X
    // WS on Y
    public void Move(Vector2 value)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.z = value.x;
        moveDir.x = -value.y;

        rb.velocity = moveDir * moveSpeed; 

    }
    
    public void Jump()
    {
        rb.velocity += Vector3.up * jumpSpeed;
    }

}
