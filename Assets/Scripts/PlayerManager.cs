using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerManager : Pawn
{
    public int playerNum = 1; 
    public float moveSpeed = 10f;
    public float rotationRate = 180f;

    


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
   
    }


    // Left Stick Mapping 
    // A/D on X
    // WS on Y
    public void Move(Vector2 value)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.z = value.x;
        moveDir.x = -value.y;

        rb.velocity = moveDir * moveSpeed; 


    }
    public void Vertical(float value)
    {
        float usevalue = value;
        if (usevalue < 0)
        {
            usevalue = usevalue * .5f;
        }

        if (rb)
        {
            rb.velocity = gameObject.transform.forward * moveSpeed * usevalue;
        }

    }

    public void Horizontal(float value)
    {
        gameObject.transform.Rotate(Vector3.up * rotationRate * value * Time.deltaTime);
    }


   
}
