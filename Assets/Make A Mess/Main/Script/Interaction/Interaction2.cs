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

        if(Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            if(hit.transform.gameObject.tag == "Grab")
            {
                ActiveGrabUI();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Interactible>().Interact(trsPlayerGuizmo, trsPlayerSpecial);
                    GrabUI.SetActive(false);
                }
            }
            else
            {
                DesactivUI();
            }
            
            if(hit.transform.gameObject.tag == "Bouton")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();
                }
            }

            if(hit.transform.gameObject.tag == "Pcprefabtag")
            {
                HitUI.SetActive(true);
                if(Input.GetMouseButton(0))
                {
                    Destroy(hit.transform.gameObject);
                    HitUI.SetActive(false);
                }
            }
        }
        else 
        {
            DesactivUI();
        }

        if(InteractibleScript.Grabed == true)
        {
            Debug.Log("Grabed");
        }
    }


    public void ActiveHitUI()
    {
        HitUI.SetActive(true);
        GrabUI.SetActive(false);
    }

    public void ActiveGrabUI()
    {
        GrabUI.SetActive(true);
        HitUI.SetActive(false);
    }

    public void DesactivUI()
    {
        GrabUI.SetActive(false);
        HitUI.SetActive(false);
    }
}