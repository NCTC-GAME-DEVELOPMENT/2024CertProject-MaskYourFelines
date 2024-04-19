using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerManager_2 : Pawn
{
    public int playerNum = 1; 
    public float moveSpeed = 10f;
    public float jumpSpeed = 20f;
    public Animator anim;
    public SpriteRenderer player;

    public bool WalkRight = false;
    public bool WalkLeft = false;
    public float timer = 0;
    public float keyDelay = .2f;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetAxis("Horizontal") > 0f)
        {
            WalkLeft = false;
            WalkRight = true;
            player.flipX = false;
            anim.SetBool("WalkRight", WalkRight);
        }
        else
        {
            WalkRight = false;
            anim.SetBool("WalkRight", WalkRight);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            
            WalkRight = false;
            WalkLeft = true;
            player.flipX = true;
            anim.SetBool("WalkLeft", WalkLeft);
        }
        else
        {
            WalkLeft = false;
            anim.SetBool("WalkLeft", WalkLeft);
        }
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
