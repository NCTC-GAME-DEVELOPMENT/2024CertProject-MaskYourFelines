using UnityEngine;

public class Grunt : EnemyBase
{
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject sprite;
    private Animator animator;

    private float windup = 0.5f;
    private float attackTime = 0.333f;
    private float activeTime = 0.1f;
    private float idleTime = 2f;
    private bool trigger = false;

    protected override void InitializeObject()
    {
        base.InitializeObject();
        animator = sprite.GetComponent<Animator>();
        animator.SetTrigger("isWalking");
    }

    protected override void MoveToPlayer()
    {
        if (!trigger)
        {
            trigger = true;
            animator.SetTrigger("isWalking");
        }

        base.MoveToPlayer();

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 3)
        {
            trigger = false;
            navMeshAgent.SetDestination(transform.position);
            animator.SetTrigger("isIdle");
            think = Attack;
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
                if(activeTime <= 0)
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

        if (distanceToPlayer > 3)
        {
            windup = 0.5f;
            attackTime = 0.333f;
            activeTime = 0.1f;
            animator.SetTrigger("isWalking");
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
            idleTime = 2;
            trigger = false;
            think = MoveToPlayer;
        }
    }
}
