using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarme_Incendie : MonoBehaviour
{
    [SerializeField] GameObject Alarme;
    [SerializeField] AudioSource alarmeIncendie;
    public bool feu1 = false;
    public bool feu2 = false;
    public bool feu3 = false;
    public bool feu4 = false;
    public bool feu5 = false;
    public bool feu6 = false;
    public bool feu7 = false;
    public bool feu8 = false;
    public bool feu9 = false;
    public bool feu10 = false;
    public bool feu11 = false;

    [SerializeField] public ParticleSystem Fire1;
    [SerializeField] public ParticleSystem Fire2;
    [SerializeField] public ParticleSystem Fire3;

    public bool alarmeActive = false;
    public bool briquetmain = false;
  
    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (briquetmain) 
                {
                    Renderer objectRenderer = hit.collider.GetComponent<Renderer>();

                    if (objectRenderer != null)
                    {
                        if (hit.collider.CompareTag("Plante1"))
                        {
                            feu1 = true;
                            Fire1.Play();
                        }
                        else if (hit.collider.CompareTag("Plante2"))
                        {
                            feu2 = true;
                            Fire2.Play();
                        }
                        else if (hit.collider.CompareTag("Plante3"))
                        {
                            feu3 = true;
                            Fire3.Play();
                        }
                        else if (hit.collider.CompareTag("Plante4"))
                        {
                            feu4 = true;

                        }
                        else if (hit.collider.CompareTag("Plante5"))
                        {
                            feu5 = true;

                        }
                        else if (hit.collider.CompareTag("Plante6"))
                        {
                            feu6 = true;

                        }
                        else if (hit.collider.CompareTag("Plante7"))
                        {
                            feu7 = true;

                        }
                        else if (hit.collider.CompareTag("Plante8"))
                        {
                            feu8 = true;

                        }
                        else if (hit.collider.CompareTag("Plante9"))
                        {
                            feu9 = true;

                        }
                        else if (hit.collider.CompareTag("Plante10"))
                        {
                            feu10 = true;
                        }
                        else if (hit.collider.CompareTag("Plante11"))
                        {
                            feu11 = true;

                        }
                    }
                }
            }
        }
        if (feu1 && feu2 && feu3 && feu4 && feu5 && feu6 && feu7 && feu8 && feu9 && feu10 && feu11 && !alarmeActive)
        {
            alarmeIncendie.Play();
            alarmeActive = true;
        }
    }


    public void Fireparticle()
    {

    }
}