using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderScript : MonoBehaviour
{
    public PlayerManager_2 player;
    public Animator anim;
    public SpriteRenderer bystander;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z >= transform.position.z + 30 || player.transform.position.z <= transform.position.z - 30)
        {
            anim.SetTrigger("isDefault");
        }
        if (player.transform.position.z > transform.position.z + 4 && player.transform.position.z < transform.position.z + 30)
        {
            anim.SetTrigger("isLookRight");
        }
        if (player.transform.position.z < transform.position.z - 4 && player.transform.position.z > transform.position.z - 30)
        {
            anim.SetTrigger("isLookLeft");
        }
        if (player.transform.position.z >= transform.position.z - 4 && player.transform.position.z <= transform.position.z + 4)
        {
            anim.SetTrigger("isLookDown");
        }
    }
}
