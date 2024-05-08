using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
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
    public PlayerHealth playerHealth;
    private float groundCheckDistance = 4.530671f;

    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject attackPoint1;
    [SerializeField] private GameObject attackPoint2;
    [SerializeField] private GameObject attackPoint3;
    [SerializeField] private GameObject attackJump;

    private int damage1 = 5;
    private int damage2 = 10;
    private int damage3 = 15;
    private int damageJump = 10;

    BoxCollider collider1;
    BoxCollider collider2;
    BoxCollider collider3;
    BoxCollider colliderJump;

    public bool isGrounded = true;
    public bool WalkRight = false;
    public bool WalkLeft = false;
    public bool jumpAttack = false;

    public AudioSource source;

    public int numberOfHits = 0;
    public float lastHitTime = 0;

    private float maxHitDelay = 1;
    private int buttonPresses = 0;
    private float lastCheckTime;
    private bool inputBool;
    private bool idle = true;
    private float attackTime;
    private bool attacking = false;
    private bool aboveThree;

    float timer = 0;
    float timerSet;
    int comboHits;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider1 = attackPoint1.GetComponent<BoxCollider>();
        collider2 = attackPoint2.GetComponent<BoxCollider>();
        collider3 = attackPoint3.GetComponent<BoxCollider>();
        colliderJump = attackJump.GetComponent<BoxCollider>();
        anim.SetBool("isKnockedDown", false);
    }

    public void Update()
    {

        if (Input.GetAxis("Horizontal") > 0f)
        {
            WalkLeft = false;
            WalkRight = true;
            //player.flipX = false;
            transform.eulerAngles = new Vector3(0, 270, 0);
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
            //player.flipX = true;
            transform.eulerAngles = new Vector3(0, 90, 0);
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
            collider1.enabled = false;
            collider2.enabled = false;
            collider3.enabled = false;
            colliderJump.enabled = false;
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

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            isGrounded = true;
            colliderJump.enabled = false;
            if (jumpAttack == true)
            {
                jumpAttack = false;
                anim.SetBool("jumpAttack", jumpAttack);
                anim.SetBool("attack1", false);
            }
            anim.SetBool("jump", false);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("jump", true);
        }

        if (playerHealth)
        {

        }
    }

    public void FixedUpdate()
    {
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 10f * jumpValue * Time.deltaTime;
        }
    }

    /*
    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        colliderJump.enabled = false;
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
    */

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
        colliderJump.enabled = true;
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
        isGrounded = true;
        if (numberOfHits == 1)
        {
            anim.SetTrigger("attack1");
            colliderJump.enabled = false;
            isGrounded = true;
            collider1.enabled = true;
            attackTime = 0.417f;
            attacking = true;
        }
        numberOfHits = Mathf.Clamp(numberOfHits, 0, 3);

        if (numberOfHits == 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.417f)
        {
            collider1.enabled = false;
            anim.SetTrigger("attack2");
            isGrounded = true;
            colliderJump.enabled = false;
            BoxCollider collider = attackPoint2.GetComponent<BoxCollider>();
            collider2.enabled = true;
            attackTime = 0.417f;
            attacking = true;
        }
        if (numberOfHits >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.417f && !aboveThree)
        {
            collider2.enabled = false;
            anim.SetTrigger("attack3");
            isGrounded = true;
            colliderJump.enabled = false;
            BoxCollider collider = attackPoint3.GetComponent<BoxCollider>();
            collider3.enabled = true;
            attackTime = 0.750f;
            attacking = true;
            if (numberOfHits > 3)
            {
                aboveThree = true;
                collider3.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyBase eb = other.gameObject.GetComponentInParent<EnemyBase>();

        if (other.CompareTag("Enemy") && collider1.enabled == true)
        {
            eb.TakeDamage(damage1);
        }
        if (other.CompareTag("Enemy") && collider2.enabled == true)
        {
            eb.TakeDamage(damage2);
        }
        if (other.CompareTag("Enemy") && collider3.enabled == true)
        {
            eb.TakeDamage(damage3);
        }
        if (other.CompareTag("Enemy") && colliderJump.enabled == true)
        {
            eb.TakeDamage(damageJump);
        }
    }
}
