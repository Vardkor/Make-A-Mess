using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Interaction2 : MonoBehaviour
{
    public Transform trsPlayerGuizmo;
    public Transform trsPlayerSpecial;

    public Image GrabUI;

    public Image HitUI;

    public Interactible InteractibleScript;



    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            if (hit.transform.CompareTag("Grab"))
            {
                Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();

                if(interactible != null)
                {
                    GrabUI.enabled = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                    }
                    if (interactible.SpecialObject == false)
                    {
                        HitUI.enabled = true;
                        Debug.Log("Special Object");
                    }
                }
            }
            else
            {
                GrabUI.enabled = false;
                HitUI.enabled = false;  
            }

            if(hit.transform.gameObject.tag == "Bouton")
            {
                if(Input.GetKeyDown(KeyCode.E)){hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();}
            }
            
            if(hit.transform.gameObject.tag == "Pcprefabtag")
            {
                if(Input.GetMouseButtonDown(0)){Destroy(hit.collider.gameObject);}
            }
        }
    }
}