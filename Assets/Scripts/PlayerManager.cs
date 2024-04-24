using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerManager : Pawn
{
    public int playerNum = 1; 
    public float moveSpeed = 10f;
    public float jumpSpeed = 20f;
    public float gravityForce;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity -= gravityForce * Vector3.up;
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

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }


    public void Jump()
    {
        rb.velocity += Vector3.up * jumpSpeed;
    }

}
