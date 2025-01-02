using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objet : MonoBehaviour
{
    //Script\\

    [SerializeField] Interaction interaction;
    [SerializeField] Alarme_Incendie alarmeincendie;
    [SerializeField] BombePeinture bombepeinture;
    [SerializeField] Clim climscript;
    [SerializeField] Tutorial_Manager tutorialscript;
    [SerializeField] Timer timerscript;

    //GameObject\\

    [SerializeField] private GameObject hache_main;
    [SerializeField] private GameObject Pieds_De_Biche_Main;
    [SerializeField] GameObject key;
    [SerializeField] GameObject Door;
    [SerializeField] public ParticleSystem FireParticle;
    
    //Bool\\
    public bool collisionEnter;
    public bool Cassable; 
    public bool PeutetreBruler;
    public bool PeutetrePeint;
    public bool BouttonClim;
    public bool PorteOuverte;
    public bool boxTimer;
    public bool TimeExit;


    //Timer\\
    [SerializeField] float remainingTime;
    public bool StartTimer;
    public bool timerStarter;
    



    private Rigidbody rb;

    void Update()
    {   
        GameObject hache = hache_main.gameObject;
        GameObject PDB = Pieds_De_Biche_Main.gameObject;
        

        if(collisionEnter && Input.GetMouseButton(0) && Cassable)  
        {
            interaction.BreakObjectHache(hache);
            interaction.BreakObjectPDB(PDB);
        }

        if(collisionEnter && Input.GetMouseButton(0) && PeutetrePeint)
        { 
            bombepeinture.Peindre();
        }

        if(collisionEnter && BouttonClim)
        { 
            climscript.SoundUp();
        }

        if(StartTimer)
        {
            TimeFire();
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
            boxTimer = false;
            
        }
        else if(other.CompareTag("Plante1"))
        {
            if(alarmeincendie.feu1 && PeutetreBruler)
            {
                StartTimer = true;
            }
        }
        else if(!boxTimer)
        {
            TimeExit = true;
            timerscript.TimeExit();
        }
        else
        {
            collisionEnter = true;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        collisionEnter = false;
    }
    

    public void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Collider>().CompareTag("ballegolf"))
        {
            Debug.Log("In the trou");
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

    public void TimeFire()
    {
        remainingTime -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if(seconds == 0.000)
        {
            FireParticle.Play();
        }
    }
}
