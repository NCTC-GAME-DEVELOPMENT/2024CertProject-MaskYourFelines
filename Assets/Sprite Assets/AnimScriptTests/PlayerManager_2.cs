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

    public bool isGrounded = true;
    public bool WalkRight = false;
    public bool WalkLeft = false;
    public bool jumpAttack = false;
    public bool attack1 = false;


    public float timer = 0;
    public float keyDelay = .2f;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    public void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 50 * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        anim.SetBool("isGrounded", isGrounded);
    }
    private void OnCollisionExit()
    {
        isGrounded = false;
        anim.SetBool("isGrounded", isGrounded);
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
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
    }

    public void Attack()
    {
        anim.SetBool("attack1", attack1);

    }

    public void JumpAttack()
    {
        anim.SetBool("jumpAttack", jumpAttack);
    }

}
