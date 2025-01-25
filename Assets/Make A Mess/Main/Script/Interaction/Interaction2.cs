using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction2 : MonoBehaviour
{
    public Transform trsPlayerGuizmo;

    public GameObject go;

    public GameObject GrabUI;

    public GameObject HitUI;

    public Interactible InteractibleScript;

    /*void Start()
    {
        InteractibleScript = GetComponent<Interactible>();
    }*/

    public void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            if(hit.transform.gameObject.tag == "Grab")
            {
                go = hit.transform.gameObject;
                go.GetComponent<Outline>().enabled = true;

                if(InteractibleScript.SpecialObject == false)
                {
                    GrabUI.SetActive(true);
                    HitUI.SetActive(false);
                }
                if(InteractibleScript.SpecialObject == true)
                {
                    HitUI.SetActive(true);
                }

                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Interactible>().Interact(trsPlayerGuizmo);
                    GrabUI.SetActive(false);
                }
            }
            else
            {
                GrabUI.SetActive(false);
                HitUI.SetActive(false);

                if(go != null)
                {
                    go.GetComponent<Outline>().enabled = false;
                    go = null;
                }
            }
            
            if(hit.transform.gameObject.tag == "Bouton")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();
                }
            }

            if(hit.transform.gameObject.tag == "PcPrefabtag")
            {
                HitUI.SetActive(true);

                if(Input.GetMouseButton(0))
                {
                    hit.collider.gameObject.GetComponent<Interactible>().Interact();
                    HitUI.SetActive(false);
                }
            }

        }
        else 
        {
            GrabUI.SetActive(false);
            HitUI.SetActive(false);

            if(go != null)
            {
                go.GetComponent<Outline>().enabled = false;
                go = null;
            }
        }
    }
}