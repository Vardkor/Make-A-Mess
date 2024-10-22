using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{

    [SerializeField] GameObject key1;
    [SerializeField] GameObject key2;
    [SerializeField] GameObject key3;
    [SerializeField] GameObject key4;
    [SerializeField] GameObject key5;
    [SerializeField] GameObject Door;

    public bool key1T = false;
    public bool key2T = false;
    public bool key3T = false;
    public bool key4T = false;
    public bool key5T = false;

    public bool porteOuverte = false;
    
    void Update()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    key1T = true;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Key2"))
                {
                    key2T = true; 
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Key3"))
                {
                    key3T = true;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Key4"))
                {
                    key4T = true;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Key5"))
                {
                    key5T = true;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (key1T && key2T && key3T && key4T && key5T && !porteOuverte)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        porteOuverte = true;
        Door.transform.Rotate(0, 90f, 0);
    }
}
