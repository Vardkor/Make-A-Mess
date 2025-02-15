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

    private Interactible currentInteractible;

    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            bool uiActivated = false;
            Interactible interactible = hit.collider.gameObject.GetComponentInParent<Interactible>();
            
            if (interactible != null && hit.transform.CompareTag("Grab"))
            {
                if(currentInteractible != interactible)
                {
                    currentInteractible = interactible;
                }
                if(!currentInteractible.Grabed)
                {
                    ActiveUI(GrabUI);
                    uiActivated = true;
                }
                else
                {
                    DesactivateCurrentUI();
                    uiActivated = false;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentInteractible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                }
                else{ActiveUI(GrabUI); uiActivated = true;}
            }
            /*else if(interactible.itemType == Interactible.eItemtype.PDB)
            {
                Debug.Log("PDB"); ERREUR AU LANCEMENT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }*/


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

            if(!uiActivated) {DesactivateCurrentUI(); currentInteractible = null;}
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