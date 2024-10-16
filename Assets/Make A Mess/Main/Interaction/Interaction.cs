using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform GrabMask;  
    public bool Grabed = false; 
    private Transform grabbedObject;
    private void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if (!Grabed) 
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                if (hit.collider.CompareTag("Grab"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        grabbedObject = hit.transform;  
                        grabbedObject.position = GrabMask.position; 
                        grabbedObject.SetParent(GrabMask); 
                        Grabed = true; 
                        //rb.isKinematic = true;
                    }
                }
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                grabbedObject.SetParent(null); 
                grabbedObject = null; 
                Grabed = false; 


            }
        }
    }
}
