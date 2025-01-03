 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Securite : MonoBehaviour
{
    [SerializeField] public Vitre vitre;
    [SerializeField] public AudioSource Alarme;
    [SerializeField] public GameObject Grille;
    public bool AlarmeActiv = false;
    public bool card = false;

    public void Start()
    {
        AlarmeActiv = true;
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
                if(card)
                {
                    Alarme.Stop();
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grab")) 
        {
            if(card)
            {
                AlarmeActiv = false;
                Grille.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else 
            {
                Debug.Log("Va checher la carte d'acces dans le bureau du patron");
            }
        }
    }

    public void Card()
    {
        card = true;
    }   
}
