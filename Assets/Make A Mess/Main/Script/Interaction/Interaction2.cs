using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Interaction2 : MonoBehaviour
{
    [SerializeField] private Transform trsPlayerGuizmo;
    [SerializeField] private Transform trsPlayerSpecial;

    public GameObject GrabUI;
    public GameObject PressButton;

    private GameObject currentUI;

    private bool uiActivated = false;
    public bool hasGrabbedObject = false;

    //private Interactible currentInteractible;

    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();

            if (hit.transform.CompareTag("Grab"))
            {
                uiActivated = true;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                }

                if(uiActivated && !hasGrabbedObject)
                {
                    ActiveUI(GrabUI);
                }
                else
                { DesactivateCurrentUI(); uiActivated = false; }
            }

            else if(hit.transform.CompareTag("Bouton"))
            {
                ActiveUI(PressButton);
                uiActivated = true;

                if(Input.GetKeyDown(KeyCode.E))
                {hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();}
            }
            
            else if(hit.transform.gameObject.tag == "Pcprefabtag")
            {
                ActiveUI(GrabUI);
                uiActivated = true;

                if(Input.GetKeyDown(KeyCode.E)){Destroy(hit.collider.gameObject);}
            }

            else {DesactivateCurrentUI(); uiActivated = false;}
        }
        if(hasGrabbedObject)
        {
            DesactivateCurrentUI();
            uiActivated = false;
        }
    }

    void ActiveUI(GameObject ui)
    {
        if (currentUI != null && currentUI != ui)
        {
            currentUI.SetActive(false);
        }

        if (ui != null)
        {
            ui.SetActive(true);
            currentUI = ui;
        }
    }

    void DesactivateCurrentUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false);
            currentUI = null;
        }
    }
}