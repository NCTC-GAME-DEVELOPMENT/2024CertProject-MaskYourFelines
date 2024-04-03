using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 5f;
    public float Damage = 5f;
    public float Health = 10f;
    public float rotationSpeed = 1f;
    

    private Rigidbody m_Rb;

    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    } 

    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horiz, 360, vert).normalized;

        if (move == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(move);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.fixedDeltaTime);
        m_Rb.MovePosition(m_Rb.position + move * speed * Time.fixedDeltaTime);
        m_Rb.MoveRotation(targetRotation);

    }

}
