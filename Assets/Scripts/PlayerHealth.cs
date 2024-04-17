using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;

    public HealthBar healthBar;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.setmaxhealth(maxHealth);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            takedamage(20);
        }
    }

    public void takedamage(int damage)
    {
        CurrentHealth -= damage;

        healthBar.SetHealth(CurrentHealth);
    }
}
