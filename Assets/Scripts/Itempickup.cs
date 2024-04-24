using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : MonoBehaviour
{
     bool ismask = false;
     bool isweapon = false;
    bool ishealthitem = false;
    public GameObject healthitem;
    public GameObject weapon;
    public GameObject mask;
    int heal = 0;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       if (Input .GetKeyDown(KeyCode.E))
        {
            if (ismask == true)
            {
                
            }
            if (isweapon == true)
            {


            }
            if(ishealthitem == true)
            {
                Destroy(healthitem);
                heal = 20;
            }
        }
    }

}
