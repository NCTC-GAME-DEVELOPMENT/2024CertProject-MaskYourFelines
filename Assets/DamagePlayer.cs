using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;
    private int damage;

    private void Start()
    {
        damage = enemyObj.GetComponent<EnemyBase>().damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().CurrentHealth -= damage;
            Debug.Log("DAMAGED PLAYER (" + other.GetComponent<PlayerHealth>().CurrentHealth + " HP)");
        }
    }
}
