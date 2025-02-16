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

    public bool hasGrabbedObject = false;

    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            bool uiActivated = false;

            Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();

            if (hit.transform.CompareTag("Grab") && !hasGrabbedObject)
            {
                ActiveUI(GrabUI);
                uiActivated = true;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    hasGrabbedObject = !hasGrabbedObject;
                    
                    if(hasGrabbedObject)
                    {
                        interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                        Debug.Log("E");
                        DesactivateCurrentUI();
                        uiActivated = false;
                    }
                    else if(!hasGrabbedObject)
                    {
                        Debug.Log("E 2");
                        ActiveUI(GrabUI);
                        uiActivated = true;
                    }
                }
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