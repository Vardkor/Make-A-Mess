using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform GrabMask;

    public bool Grabed = false;

    private void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (hit.collider.CompareTag("Grab"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.position = (GrabMask.position);
                    Grabed = true;
                    //Debug.Log("Tu as grab l'objet");
                }

            }
           
        }
    }
}
