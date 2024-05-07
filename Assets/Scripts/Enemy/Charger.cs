using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : EnemyBase
{
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject attackPoint1;
    [SerializeField] private GameObject attackPoint2;
    [SerializeField] private GameObject sprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private int chargeDamage = 10;

    private float windup = 0.5f;
    private float attackTime = 0.333f;
    private float activeTime = 0.1f;
    private float idleTime = 2f;
    private float chargeDelay = 1f;
    private float hitStunTimer = 0f;

    private int doSomething = 0;

    private bool dead = false;
    private bool charging = false;
    private bool trigger = false;

    private float blinkDuration = 1f;
    private float blinkInterval = 0.2f;
    private Coroutine blinkCoroutine;

    private Vector3 chargePoint;

    protected override void InitializeObject()
    {
        base.InitializeObject();
        health = 40;
        damage = 5;
        animator = sprite.GetComponent<Animator>();
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        think = MoveToPlayer;
    }

    protected override void Update()
    {
        base.Update();

        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);

        Vector3 directionToPlayer = playerObj.transform.position - transform.position;

        if (directionToPlayer.z < 0f && !charging)
        {
            spriteRenderer.flipX = false;
            hitBox.transform.position = attackPoint2.transform.position;
        }
        if (directionToPlayer.z > 0f && !charging)
        {
            spriteRenderer.flipX = true;
            hitBox.transform.position = attackPoint1.transform.position;
        }

        if (hitStunTimer > 0f)
        {
            hitStunTimer -= Time.deltaTime;
            if (hitStunTimer < 0f)
            {
                ResetTimers();
                think = MoveToPlayer;
                if (health <= 0)
                {
                    dead = true;
                    ResetTimers();
                    think = Death;
                }
            }
        }
    }
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 6)
        {
            ResetTimers();
            trigger = false;
            navMeshAgent.SetDestination(transform.position);
            think = Attack;
        }

        else if(distanceToPlayer >= 20)
        {
            trigger = false;
            think = Charge;
        }
        else if (!trigger)
        {
            trigger = true;
            animator.SetTrigger("isWalking");
        }
    }

    protected override void Attack()
    {
        windup -= Time.deltaTime; //0.5
        if (windup <= 0)
        {
            attackTime -= Time.deltaTime; //0.333
            if (!trigger)
            {
                trigger = true;
                animator.SetTrigger("isAttacking");
            }
            if (attackTime <= 0)
            {
                activeTime -= Time.deltaTime; //0.1
                BoxCollider collider = hitBox.GetComponent<BoxCollider>();
                collider.enabled = true;
                if (activeTime <= 0)
                {
                    windup = 0.5f;
                    attackTime = 0.333f;
                    activeTime = 0.1f;
                    trigger = false;
                    collider.enabled = false;
                    think = Idle;
                }
            }
        }

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer > 6)
        {
            windup = 0.5f;
            attackTime = 0.333f;
            activeTime = 0.1f;
            trigger = false;
            think = MoveToPlayer;
        }
    }

    private void Idle()
    {
        if (!trigger)
        {
            trigger = true;
            animator.SetTrigger("isIdle");
        }
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                idleTime = 2f;
                trigger = false;
                think = MoveToPlayer;
            }
            if (random == 2)
            {
                idleTime = 2f;
                trigger = false;
            }
        }
    }

    private void Charge()
    {
        if (!trigger)
        {
            charging = true;
            trigger = true;
            gameObject.GetComponent<Collider>().isTrigger = true;
            navMeshAgent.speed = 10;
            navMeshAgent.SetDestination(playerObj.transform.position);
            chargePoint = playerObj.transform.position;
            animator.SetTrigger("isCharging");
        }

        float distanceToPoint = Vector3.Distance(chargePoint, transform.position);

        if (distanceToPoint <= 2)
        {
            if (charging)
            {
                charging = false;
                gameObject.GetComponent<Collider>().isTrigger = false;
                navMeshAgent.speed = 3.5f;
                animator.SetTrigger("isIdle");
            }

            chargeDelay -= Time.deltaTime; //1f
            if (chargeDelay <= 0)
            {
                trigger = false;
                chargeDelay = 1f;
                think = MoveToPlayer;
            }
        }
    }

    private void DoNothing()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void Death()
    {
        if (!trigger)
        {
            animator.SetTrigger("isDead");
            navMeshAgent.SetDestination(transform.position);
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        float timer = 0f;
        while (timer < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true;

        Destroy(gameObject);
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!dead && !charging)
        {
            animator.SetTrigger("isDamaged");
            HitStun(0.25f);
        }
    }

    private void HitStun(float hitStun)
    {
        ResetTimers();
        hitStunTimer = hitStun;
        think = DoNothing;
    }

    private void ResetTimers()
    {
        windup = 0.5f;
        attackTime = 0.333f;
        activeTime = 0.1f;
        idleTime = 2f;
        trigger = false;
    }    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && charging && other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().takeDamage(chargeDamage);
            Debug.Log("CHARGED PLAYER (" + other.GetComponent<PlayerHealth>().CurrentHealth + " HP)");
        }
    }
}
