using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objet : MonoBehaviour
{
    //Script\\
    [SerializeField] Tutorial_Manager tutorialscript;
    [SerializeField] Timer timerscript;
    
    //Bool\\
    public bool collisionEnter;
    public bool boxTimer;
    public bool TimeExit;


    //Timer\\
    [SerializeField] float remainingTime;
    public bool StartTimer;
    public bool timerStarter;

    private Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        if(boxTimer)
        {
            timerscript.StartingTimer();
            boxTimer = false;
            
        }
        else if(!boxTimer)
        {
            TimeExit = true;
            //timerscript.TimeExit();
        }
        else
        {
            collisionEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisionEnter = false;
    }
}
