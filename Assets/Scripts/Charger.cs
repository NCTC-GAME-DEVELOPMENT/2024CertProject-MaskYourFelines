using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : EnemyBase
{
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject sprite;
    private Animator animator;

    private float windup = 0.5f;
    private float attackTime = 0.333f;
    private float activeTime = 0.1f;
    private float idleTime = 2f;
    private float chargeDelay = 1f;
    [SerializeField] private int chargeDamage = 10;
    private bool charging = false;
    private bool trigger = false;
    private Vector3 chargePoint;

    protected override void InitializeObject()
    {
        base.InitializeObject();
        damage = 5;
        animator = sprite.GetComponent<Animator>();
        think = Charge;
    }

    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 6)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && charging && other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().CurrentHealth -= chargeDamage;
            Debug.Log("CHARGED PLAYER (" + other.GetComponent<PlayerHealth>().CurrentHealth + " HP)");
        }
    }
}
