using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Incendie : MonoBehaviour
{

    [SerializeField] GameObject plante1;
    [SerializeField] GameObject plante2;
    [SerializeField] GameObject plante3;
    [SerializeField] GameObject Alarme;
    [SerializeField] AudioSource alarmeIncendie;
    public bool feu1 = false;
    public bool feu2 = false;
    public bool feu3 = false;
    public bool alarmeActive = false;


    private void Update()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("Plante1"))
                {
                    feu1 = true;
                }
                else if (hit.collider.CompareTag("Plante2"))
                {
                    feu2 = true; 
                }
                else if (hit.collider.CompareTag("Plante3"))
                {
                    feu3 = true;
                }
            }
        }
    
        if (feu1 && feu2 && feu3 && !alarmeActive)
        {
            alarmeIncendie.Play();  
            alarmeActive = true;  
        } 
    }
}