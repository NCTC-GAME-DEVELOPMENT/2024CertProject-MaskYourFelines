using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Grunt : EnemyBase
{
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject attackPoint1;
    [SerializeField] private GameObject attackPoint2;
    [SerializeField] private GameObject sprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float windup = 0.5f;
    private float attackTime = 0.333f;
    private float activeTime = 0.1f;
    private float idleTime = 2f;
    private float hitStunTimer = 0f;

    private int doSomething = 0;

    private bool trigger = false;
    private bool dead = false;

    private float blinkDuration = 1f;
    private float blinkInterval = 0.2f;
    private Coroutine blinkCoroutine;
    protected override void InitializeObject()
    {
        base.InitializeObject();
        health = 25;
        damage = 5;
        animator = sprite.GetComponent<Animator>();
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        think = Idle;
    }

    protected override void Update()
    {
        base.Update();

        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);

        Vector3 directionToPlayer = playerObj.transform.position - transform.position;

        if (directionToPlayer.z < 0f)
        {
            spriteRenderer.flipX = false;
            hitBox.transform.position = attackPoint2.transform.position;
        }
        if (directionToPlayer.z > 0f)
        {
            spriteRenderer.flipX = true;
            hitBox.transform.position = attackPoint1.transform.position;
        }

        if (hitStunTimer > 0f)
        {
            hitStunTimer -= Time.deltaTime;
            if (hitStunTimer <= 0f)
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

        if (distanceToPlayer <= 3)
        {
            ResetTimers();
            navMeshAgent.SetDestination(transform.position);
            animator.SetTrigger("isIdle");
            think = Attack;
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
                    ResetTimers();
                    collider.enabled = false;
                    think = Idle;
                }
            }
        }

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer > 3)
        {
            ResetTimers();
            think = MoveToPlayer;
        }
    }

    private void Idle()
    {
        if (!trigger)
        {
            trigger = true;
            animator.SetTrigger("isIdle");
            doSomething++;
        }
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            int random = Random.Range(1, 3);
            if (random == 1 || doSomething == 3)
            {
                ResetTimers();
                doSomething = 0;
                think = MoveToPlayer;
            }
            if (random == 2)
            {
                ResetTimers();
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
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!dead)
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
}