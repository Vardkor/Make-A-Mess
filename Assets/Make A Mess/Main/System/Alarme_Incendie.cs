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


    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == plante1)
        {
            feu1 = true;

        }
        else if(other.gameObject== plante2)
        {
            feu2 = true;

        }
        else if (other.gameObject== plante3)
        {
            feu3 = true;

        }
    }


    void Update()
    {
        
        if (feu1 && feu2 && feu3 && !alarmeActive)
        {
            alarmeIncendie.Play();  
            alarmeActive = true;  
        }
    }

}
