using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyBase
{
    private float windup = 0.5f;
    private bool attacking = false;
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
        
        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 3)
        {
            think = Attack;
        }
    }

    protected override void Attack()
    {
        windup -= Time.deltaTime;
        if (windup <= 0 && !attacking)
        {
            attacking = true;
            //ATTACK
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
        //slowly move away from player
        think = MoveToPlayer;
    }
}
