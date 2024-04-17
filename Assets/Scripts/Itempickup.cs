using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : MonoBehaviour
{
    public PlayerHealth health;
    public GameObject healthitem;
    public GameObject weapon;
    public GameObject mask;
    public int heal = 20;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
