using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderScript : MonoBehaviour
{
    public PlayerManager_2 player;
    public Animator anim;
    public SpriteRenderer bystander;
    public bool idle = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (idle)
        {
            anim.SetTrigger("isDefault");
            idle = false;
        }
        if (player.transform.position.z > transform.position.z + 3)
        {
            anim.SetTrigger("isLookRight");
        }
        if (player.transform.position.z < transform.position.z - 3)
        {
            anim.SetTrigger("isLookLeft");
        }
        if (player.transform.position.z >= transform.position.z - 3 && player.transform.position.z <= transform.position.z + 3)
        {
            anim.SetTrigger("isLookDown");
        }
    }
}
