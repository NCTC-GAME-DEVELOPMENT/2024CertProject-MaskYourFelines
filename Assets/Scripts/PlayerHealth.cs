using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;
    public PlayerManager_2 player;

    public HealthBar healthBar;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.setmaxhealth(maxHealth);
    }

    
    void Update()
    {
       
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        player.anim.SetTrigger("isDamaged");
        healthBar.SetHealth(CurrentHealth);
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
