using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    /// <summary>
    /// Show Input for this controler, Super Spammy when true. 
    /// </summary>
    public bool LogInputStateInfo = false; 

    protected InputPoller inputPoller; 
    protected InputData InputCurrent;
    protected InputData InputPrevious;


    protected override void Start()
    {
        base.Start();
        IsHuman = true; 

        inputPoller = InputPoller.Self; 
        if (!inputPoller)
        {
            LOG_ERROR("****PLAYER CONTROLER: No Input Poller in Scene");
            return; 
        }
      
    }

    protected void Update()
    {
        GetInput();
        ProcessInput();
        InputPrevious = InputCurrent;
    }

    

    protected virtual void GetInput()
    {
        if (!inputPoller)
        {
            LOG_ERROR("****PLAYER CONTROLER (" + gameObject.name + "): No Input Poller in Scene");
            return;
        }
        
        InputCurrent = InputPoller.Self.GetInput(InputPlayerNumber);

        if (LogInputStateInfo)
        {
            LOG(InputCurrent.ToString());
        }
       
    }



    protected virtual void ProcessInput()
    {

    }



    public virtual void Horizontal(float value)
    {
        if (value != 0)
        {
            LOG("Del-Horizontal (" + value + ")");
        }
    }

    public virtual void Vertical(float value)
    {
        if (value != 0)
        {
            LOG("Del-Vertical (" + value +")");
        }       
    }

    public virtual void Fire1(bool value)
    {
        if (value)
        {
            LOG("Del-Fire1");
        }
    }

    public virtual void Fire2(bool value)
    {
        if (value)
        {
            LOG("Del-Fire2");
        }
    }

    public virtual void Fire3(bool value)
    {
        if (value)
        {
            LOG("Del-Fire3");
        }
    }

    public virtual void Fire4(bool value)
    {
        if (value)
        {
            LOG("Del-Fire4");
        }
    }
}
