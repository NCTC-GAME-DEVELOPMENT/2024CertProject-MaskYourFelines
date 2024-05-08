using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject playerObj;
    protected PlayerManager_2 playerManager;
    public Vector3 playerTransform;

    public int health;
    public int damage;

    protected delegate void ThinkFunction();
    protected ThinkFunction think;

    void Start()
    {
        InitializeObject();    
    }

    protected virtual void InitializeObject()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerManager = playerObj.GetComponent<PlayerManager_2>();
        think = MoveToPlayer;
    }

    protected virtual void Update()
    {
        think?.Invoke();
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(5);
        }
    }

    protected virtual void MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerObj.transform.position);
    }
    protected virtual void Attack()
    {

    }
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
