using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Pawn
{

    public float moveSpeed = 10f;
    public float rotationRate = 180f;

    
    private CharacterController _characterController;

    private Vector2 _playerMovementInput;

    private void Start()
    {

        _characterController = GetComponent<CharacterController>();
        
    }


}
