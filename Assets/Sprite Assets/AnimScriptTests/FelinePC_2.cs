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
        Debug.Log("Input Move");
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
                Debug.Log("Input Jump");
            }

            if (player.isGrounded == false && InputCurrent.buttonEast)
            {
                player.jumpAttack = true;
                player.JumpAttack();
            }
    }

    public void Attack(bool value)
    {
        if (value && player.isGrounded == true)
        {
            Debug.Log("Input Attack");
            player.ComboAttack();
        }

        /*
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
        if (InputCurrent.buttonEast && player.isGrounded == true && player.attack1 == true)
        {
            player.attack2 = true;
            player.Attack2();
        }
        else
        {
            player.attack2 = false;
            player.Attack2();
        }
        */

    }

    public void Test(bool value)
    {
        if (value)
        {
            player.Test(InputCurrent.buttonEast);
        }
    }
}
