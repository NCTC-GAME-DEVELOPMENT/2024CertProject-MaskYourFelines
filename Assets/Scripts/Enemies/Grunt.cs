using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyBase
{
  
    public override void InitalizeObject()
    {
        base.InitalizeObject();
        think = Chase;
    }

   

    private void Chase()
    {
        MoveToPlayer();
        distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);
        if (distanceToPlayer <= 4)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            think = Attack;
        }
    }

    private void Attack()
    {
        //Player grace timer
        //DrawCircle (Attack)
        //Set to Idle
        distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);
        if (distanceToPlayer >= 5) 
        {
            think = Chase;
        }
    }

    private void Idle()
    {

    }
}
