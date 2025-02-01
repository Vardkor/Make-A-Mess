using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Interaction2 : MonoBehaviour
{
    public Transform trsPlayerGuizmo;
    public Transform trsPlayerSpecial;

    public GameObject GrabUI;

    public GameObject HitUI;

    public Interactible InteractibleScript;



    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            if (hit.transform.CompareTag("Grab"))
            {
                Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();

                if (interactible != null) 
                {
                    if (interactible.SpecialObject) 
                    {
                        // Affiche GrabUI si c'est un objet spécial
                        GrabUI.SetActive(true);
                        HitUI.SetActive(false);
                    } 
                    else
                    {
                        // Affiche HitUI si c'est un objet normal
                        GrabUI.SetActive(false);
                        HitUI.SetActive(true);
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                    GrabUI.SetActive(false);
                }
            else
            {
                GrabUI.SetActive(false);
                HitUI.SetActive(false);
            }
        }

            if(hit.transform.gameObject.tag == "Bouton")
            {
                if(Input.GetKeyDown(KeyCode.E)){hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();}
            }

            else
            {
                GrabUI.SetActive(false);
                HitUI.SetActive(false);
            }
        }
    }
}