using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : PlayerHealth
{
     bool ismask = false;
     bool isweapon = false;
    bool ishealthitem = false;
    public GameObject healthitem;
    public GameObject weapon;
    public GameObject mask;
   
    void Start()
    {
       
        
    }

   
    void Update()
    {
        
    }

    void healthitemgot(int heal)
    {
        CurrentHealth += heal;
    }


}
