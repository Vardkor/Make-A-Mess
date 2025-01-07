using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction2 : MonoBehaviour
{
    public Transform trsPlayerGuizmo;
    public void Update()
    {
        RaycastHit hit;
    
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, 5f))
            {
                if(hit.collider.tag == "Grab")
                {
                    hit.collider.gameObject.GetComponent<Interactible>().Interact(trsPlayerGuizmo);
                }
            }
        }
    }
}