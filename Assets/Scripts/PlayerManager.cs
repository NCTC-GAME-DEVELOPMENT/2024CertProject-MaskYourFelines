using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerManager : MonoBehaviour
{
    public int playerNum = 1; 
    public float moveSpeed = 10f;
    public float rotationRate = 180f;


    public InputData input;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = InputPoller.Self.GetInput(playerNum); 
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
