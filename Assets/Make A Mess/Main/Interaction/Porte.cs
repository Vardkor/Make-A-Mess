using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    private bool OpenPorte;
    void OnTriggerStay(Collider other)
    { 
        OpenPorte = true; 
    }
    void OnTriggerExit(Collider other)
    {        
        OpenPorte = false;
    }
    void Update()
    {
        if (OpenPorte == true)
        {
            transform.Rotate(0, 90f, 0);
        }
        else if(OpenPorte == false)
        {
           transform.Rotate(0, 0, 0); 
        }
    }

}
