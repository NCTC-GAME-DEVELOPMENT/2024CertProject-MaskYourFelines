using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelinePC : PlayerController
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
        Jump(InputCurrent.lefterStick);
    }


    public void Move(Vector2 value)
    {
        player.Move(value); 
    }

    public void Jump(Vector3 value)
    {
        player.Jump(value);
    }
}
