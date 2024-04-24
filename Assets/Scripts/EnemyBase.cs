using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject playerObj;
    protected PlayerManager playerManager;

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
        playerObj = FindObjectOfType<PlayerManager>().gameObject;
        playerManager = playerObj.GetComponent<PlayerManager>();
        think = MoveToPlayer;
    }
    // Update is called once per frame
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
    protected virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
