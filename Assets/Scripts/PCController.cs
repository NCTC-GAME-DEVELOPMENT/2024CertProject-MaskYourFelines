using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : PlayerController 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void ProcessInput()
    {
        
        Vertical(InputCurrent.leftStick.y);
        Horizontal(InputCurrent.leftStick.x);
    }

}
