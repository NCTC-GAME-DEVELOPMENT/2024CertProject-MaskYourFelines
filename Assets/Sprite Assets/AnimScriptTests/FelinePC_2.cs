using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelinePC_2 : PlayerController
{
    public PlayerManager player; 
    

    protected override void Start()
    {
        base.Start();
        ControlPawn(player); 
    }

    protected override void ProcessInput()
    {
        Move(InputCurrent.leftStick);

        Jump(InputCurrent.buttonSouth);
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
            if (value)
            {
                player.Jump();
            }
    }
}
