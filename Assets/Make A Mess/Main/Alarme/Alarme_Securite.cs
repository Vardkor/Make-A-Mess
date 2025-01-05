 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Securite : MonoBehaviour
{
    [SerializeField] public List<Vitre> vitres;
    [SerializeField] public AudioSource Alarme;
    public bool AlarmeActiv = false;

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
            if (Alarme.isPlaying)
            {
                Alarme.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(AlarmeActiv)
        {
            AlarmeActiv = false;
        }
    }

}
