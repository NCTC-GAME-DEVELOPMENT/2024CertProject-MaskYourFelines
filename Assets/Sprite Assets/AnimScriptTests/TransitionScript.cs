using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public float keyDelay = .2f;
    public float nextFireTime = 0f;
    public int numberOfHits = 0;
    public float lastHitTime = 0;

    float maxHitDelay = 1;
    public int buttonPresses = 0;
    public float lastCheckTime;

    public PlayerManager_2 player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.anim;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastHitTime > maxHitDelay)
        {
            numberOfHits = 0;
        }

        
    }
    public void ComboTransition1()
    {
        if (numberOfHits >= 2)
        {
            anim.SetTrigger("attack2");
        }
    }
    public void ComboTransition2()
    {
        if (numberOfHits >= 3)
        {
            anim.SetTrigger("attack3");
        }
    }
}
