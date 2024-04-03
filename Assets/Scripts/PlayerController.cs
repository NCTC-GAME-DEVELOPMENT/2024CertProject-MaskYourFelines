using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 5f;
    public float Damage = 5f;
    public float Health = 10f;
    public float rotationSpeed = 1f;

    private CharacterController controller;
   
   
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        
        

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move == Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

    }

}
