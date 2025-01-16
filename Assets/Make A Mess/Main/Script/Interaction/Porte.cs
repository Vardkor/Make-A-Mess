using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{

    [SerializeField] GameObject key;
    [SerializeField] GameObject Door;

    public bool keyT = false;
    public bool porteOuverte = false;
    
    void Update()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if (hit.collider.CompareTag("Key"))
                //{
                    //keyT = true;
                    //Destroy(hit.collider.gameObject);
                    //Door.transform.Rotate(0, 90f, 0);
                //}
            }
        }

        if (keyT && !porteOuverte)
        {
            
            //OpenDoor();
        }
    }

    void OpenDoor()
    {
        porteOuverte = true;
        //Door.transform.Rotate(0, 90f, 0);
    }
}
