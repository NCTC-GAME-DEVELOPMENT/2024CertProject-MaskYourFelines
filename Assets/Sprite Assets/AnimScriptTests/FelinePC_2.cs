using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelinePC_2 : PlayerController
{
    public PlayerManager_2 player; 
    

    protected override void Start()
    {
        base.Start();
        ControlPawn(player); 
    }

    protected override void ProcessInput()
    {
        Move(InputCurrent.leftStick);

        Jump(InputCurrent.buttonSouth);

        Attack(InputCurrent.buttonEast);
    }


    public void Move(Vector2 value)
    {
        player.Move(value); 
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

    public void Jump(bool value)
    {
            if (value && player.isGrounded == true)
            {
                player.Jump();
            }

            if (player.isGrounded == false && InputCurrent.buttonEast)
            {
                player.jumpAttack = true;
                player.JumpAttack();
            }
            else 
            { 
                player.jumpAttack = false;
                player.JumpAttack();
            }
    }

    public void Attack(bool value)
    {
        if (InputCurrent.buttonEast && player.isGrounded == true)
        {
            player.attack1 = true;
            player.Attack();
        }
        else 
        { 
            player.attack1 = false;
            player.Attack();
        }
     
    }
}
