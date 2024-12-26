 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Securite : MonoBehaviour
{
    [SerializeField] public Vitre vitre;
    [SerializeField] public AudioSource Alarme;
    public bool AlarmeActiv = false;

    public void Start()
    {
        AlarmeActiv = true;
        Debug.Log("Activation Alarme");
    }
    void Update()
    {
        if(AlarmeActiv && vitre.vitrebreak)
        {
            if(!Alarme.isPlaying)
            {
                Alarme.Play();
            }
        }
        else if(!AlarmeActiv)
        {
            if(Alarme.isPlaying)
            {
                Alarme.Stop();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grab")) 
        {
            AlarmeActiv = false;
        }
    }
}
