using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;
    public PlayerManager_2 player;


    float endgameTimer = 0; 

    public HealthBar healthBar;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.setmaxhealth(maxHealth);
    }
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            OnDeath(); 
            player.gameOverTimer += Time.deltaTime;
            if (player.gameOverTimer >= endgameTimer)
            {
                Debug.Log("time is up");
                player.isGameOver = false;
                MainMenu.instance.MainMenuScene();
            }
        }
    }
    void OnDeath()
    {
        Debug.Log("health is zero");
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
    }
}
