using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    [SerializeField] private Light Light;
    [SerializeField] private Collider BoxHautDesMarches;
      
    public bool Boutton;
    public bool collisionEnter;

    void start()
    {
        Light.intensity = 0f;
    }


    void Update()
    {
        if(collisionEnter && Boutton)
        { 
            Light.intensity = 50f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        collisionEnter = true;
    }

    void OnTriggerExit(Collider other)
    {
        collisionEnter = false;
    }
}
