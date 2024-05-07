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

    public float keyDelay = .2f;
    public float nextFireTime = 0f;
    public int numberOfHits = 0;
    public float lastHitTime = 0;

    float maxHitDelay = 1;
    public int buttonPresses = 0;
    public float lastCheckTime;
    public bool inputBool;
    public bool idle = true;
    public float attackTime;
    public bool attacking = false;
    public bool aboveThree;

    float timer = 0;
    float timerSet;
    int comboHits;

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
            anim.SetBool("WalkRight", true);
        }
        else
        {
            WalkRight = false;
            anim.SetBool("WalkRight", false);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            WalkRight = false;
            WalkLeft = true;
            player.flipX = true;
            anim.SetBool("WalkLeft", true);
        }
        else
        {
            WalkLeft = false;
            anim.SetBool("WalkLeft", false);
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && idle)
        {
            anim.SetTrigger("isIdle");
            idle = false;
        }

        if (Time.time - lastHitTime > maxHitDelay)
        {
            numberOfHits = 0;
        }

        if (attacking)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                idle = true;
                attacking = false;
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
        anim.SetBool("jump", false);
    }
    private void OnCollisionExit()
    {
        isGrounded = false;
        anim.SetBool("jump", true);
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
        rb.AddForce(Vector3.up * jumpSpeed * 10f, ForceMode.Impulse);
        Debug.Log("Jumping Now");
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
            anim.SetTrigger("attack1");
            attackTime = 0.417f;
            attacking = true;
        }
        numberOfHits = Mathf.Clamp(numberOfHits, 0, 3);

        if (numberOfHits == 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.417f)
        {
            anim.SetTrigger("attack2");
            attackTime = 0.417f;
            attacking = true;
        }
        if (numberOfHits >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.417f && !aboveThree)
        {
            anim.SetTrigger("attack3");
            attackTime = 0.750f;
            attacking = true;
            if (numberOfHits > 3)
            {
                aboveThree = true;
            }
        }
    
    }
}
