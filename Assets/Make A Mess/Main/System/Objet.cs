using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objet : MonoBehaviour
{
    //Script\\

    [SerializeField] Interaction interaction;
    [SerializeField] Alarme_Incendie alarme;
    [SerializeField] BombePeinture bombepeinture;
    [SerializeField] Clim climscript;
    [SerializeField] Tutorial_Manager tutorialscript;
    [SerializeField] Timer timerscript;

    //GameObject\\

    [SerializeField] private GameObject hache_main;
    [SerializeField] GameObject key;
    [SerializeField] GameObject Door;
    
    //Bool\\
    public bool collisionEnter;
    public bool Cassable; 
    public bool PeutetreBruler;
    public bool PeutetrePeint;
    public bool BouttonClim;
    public bool PorteOuverte;
    public bool boxTimer;


    private Rigidbody rb;

    void Update()
    {   
        GameObject hache = hache_main.gameObject;
        
        if (collisionEnter)
        {
            if (GetComponent<Collider>().CompareTag("Plante1"))
            {
                Debug.Log("Plante est la ");
            }
        }

        if(collisionEnter && Input.GetMouseButton(0) && Cassable)  
        {
            interaction.BreakObject(hache);
        }

        if(collisionEnter && Input.GetMouseButton(0) && PeutetrePeint)
        { 
            bombepeinture.Peindre();
        }

        if(collisionEnter && BouttonClim)
        { 
            climscript.SoundUp();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("flechettes"))
        {
           Rigidbody rb = other.GetComponent<Rigidbody>();
           rb.isKinematic = true;
        }
        else if(boxTimer)
        {
            timerscript.StartingTimer();
        }
        else
        {
            collisionEnter = true;
        }
    }


    public void PortePatron()
    {
        if(!PorteOuverte)
        {
            PorteOuverte = true;
            Door.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
