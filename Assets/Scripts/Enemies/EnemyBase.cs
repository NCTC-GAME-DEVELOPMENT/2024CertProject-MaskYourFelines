using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    protected PlayerController playerObj;

    public delegate void ThinkFunction();
    protected ThinkFunction think;

    public float distanceToPlayer;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitalizeObject(); 
    }

    public virtual void InitalizeObject()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        think?.Invoke();
    }

    protected virtual void MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerObj.transform.position);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
