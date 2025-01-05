 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Securite : MonoBehaviour
{
    [SerializeField] public List<Vitre> vitres;
    [SerializeField] public AudioSource Alarme;
    [SerializeField] public GameObject Grille;
    [SerializeField] public GameObject carte;
    public bool AlarmeActiv = false;
    public bool card = false;

    public void Start()
    {
        AlarmeActiv = true;
    }
    void Update()
    {
        bool vitreCassée = false;

        foreach (Vitre vitre in vitres)
        {
            if (vitre.vitrebreak) 
            {
                vitreCassée = true;
                break;
            }
        }
        if (AlarmeActiv && vitreCassée)
        {
            if (!Alarme.isPlaying)
            {
                Alarme.Play();
            }
        }
        else if (!AlarmeActiv)
        {
            if (Alarme.isPlaying && card)
            {
                Alarme.Stop();
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
                Destroy(Grille.gameObject);
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
        Destroy(carte.gameObject);
    }   
}
