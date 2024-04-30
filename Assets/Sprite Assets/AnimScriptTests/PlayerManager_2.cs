using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager_2 : Pawn
{
    public int playerNum = 1;
    public float jumpValue = 50000f;
    public float moveSpeed = 10f;
    public float jumpSpeed = 50f;
    public Animator anim;
    public SpriteRenderer player;

    public bool isGrounded = true;
    public bool WalkRight = false;
    public bool WalkLeft = false;
    public bool jumpAttack = false;
    public bool attack1 = false;
    public bool attack2 = false;
    public bool attack3 = false;

    public float keyDelay = .2f;
    public float nextFireTime = 0f;
    public int numberOfHits = 0;
    public float lastHitTime = 0;

    float maxHitDelay = 1;
    public int buttonPresses = 0;
    public float lastCheckTime;
    public bool inputBool;

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

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            anim.SetBool("attack1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            anim.SetBool("attack2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            anim.SetBool("attack3", false);
            numberOfHits = 0;
        }

        if (Time.time - lastHitTime > maxHitDelay)
        {
            numberOfHits = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (inputBool)
            {
                inputBool = false;
                ComboAttack();
            }
        }

    }

    public void FixedUpdate()
    {
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 10f * jumpValue * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        if (jumpAttack == true)
        {
            jumpAttack = false;
            anim.SetBool("jumpAttack", jumpAttack);
            anim.SetBool("attack1", false);
        }
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
        rb.AddForce(Vector3.up * jumpSpeed * 10f, ForceMode.VelocityChange);
    }

    public void JumpAttack()
    {
        anim.SetBool("jumpAttack", jumpAttack);
    }

    //check to see if PC values can be sent to PM methods directly
    //Anwser: YES
    public void Test(bool value)
    {
        Debug.Log("IT WORKED!");
    }
    
    public void ComboAttack()
    {
        lastHitTime = Time.time;
        numberOfHits++;
        if (numberOfHits == 1)
        {
            anim.SetBool("attack1", true);
        }
        numberOfHits = Mathf.Clamp(numberOfHits, 0, 3);

        if (numberOfHits >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", true);
        }
        if (numberOfHits >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", true);
        }
    
    }

    public void SendAttackInput(bool value)
    {
        if(value)
        {
            inputBool = true;
        }
        else
        {
            inputBool = false;
        }
    }
    /*
    public void Attack()
    {
        anim.SetBool("attack1", attack1);
    }
    public void Attack2()
    {
        anim.SetBool("attack2", attack2);
    }
    public void Attack3()
    {
        anim.SetBool("attack3", attack3);
    }
    */
}
