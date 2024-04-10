using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Pawn
{

    public float moveSpeed = 10f;
    public float rotationRate = 180f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
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
