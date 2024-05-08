using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;
    public PlayerManager_2 player;


    float endgameTimer = 0;
    bool isEndGame = false; 

    public HealthBar healthBar;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.setmaxhealth(maxHealth);
    }

    
    void Update()
    {
        if (endgameTimer > 0)
        {
            endgameTimer -= Time.deltaTime; 
            if (endgameTimer  < 0 )
            {
                player.isGameOver = false;
                MainMenu.instance.MainMenuScene();
            }
        }

        if ((!isEndGame) && (CurrentHealth <= 0) )
        {
            OnDeath(); 
        }
    }

    void OnDeath()
    {
        isEndGame = true; 
        //Debug.Log("health is zero");
        player.anim.SetBool("isKnockedDown", true);
        player.isGameOver = true;
        player.GameOverScreen.SetActive(true);
        endgameTimer = player.duration; 
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        player.anim.SetTrigger("isDamaged");
        healthBar.SetHealth(CurrentHealth);
        player.anim.SetTrigger("isDamaged");
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
