using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyBase
{
    private float windup = 1f;
    private float idleTime = 2f;
    private float range = 2f;
    private bool attacking = false;

    public Transform attackPoint;
    public GameObject sprite;
    private Animator animator;

    protected override void InitializeObject()
    {
        base.InitializeObject();
        animator = sprite.GetComponent<Animator>();
        animator.SetTrigger("isWalking");
    }
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 3)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetTrigger("isIdle");
            think = Attack;
        }
    }

    protected override void Attack()
    {
        animator.SetTrigger("isAttacking");
        windup -= Time.deltaTime;
        if (windup <= 0 && !attacking)
        {
            attacking = true;
            Collider[] player = Physics.OverlapSphere(attackPoint.position, range);
            foreach(Collider col in player)
            {
                if (col.CompareTag("Player"))
                {
                    Debug.Log("Damaged Player");
                }
            }
            windup = 1f;
            think = Idle;
        }
        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer > 3)
        {
            think = MoveToPlayer;
        }
    }

    private void Idle()
    {
        animator.SetTrigger("isIdle");
        idleTime -= Time.deltaTime;
        if(idleTime <= 0)
        {
            idleTime = 2;
            think = MoveToPlayer;
        }
    }

    protected override void TakeDamage()
    {
        base.TakeDamage();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
