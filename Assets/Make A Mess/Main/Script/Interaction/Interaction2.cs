using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Interaction2 : MonoBehaviour
{
    [SerializeField] private Transform trsPlayerGuizmo;
    [SerializeField] private Transform trsPlayerSpecial;

    public Image GrabUI;
    public Image HitUI;

    private Image currentUI;

    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            bool uiActivated = false;
            
            if (hit.transform.CompareTag("Grab"))
            {
                Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();

                if(interactible != null)
                {
                    ActiveUI(interactible.SpecialObject ? HitUI : GrabUI);
                    uiActivated = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(interactible.SpecialObject==false)
                        {
                            interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                        }
                        else if (interactible.SpecialObject==true)
                        {
                            interactible.Interact(trsPlayerGuizmo, trsPlayerSpecial);
                            
                            Debug.Log("Clic Gauche");
                            ActiveUI(HitUI);
                            uiActivated = true;
                        }
                    }
                }
            }

            else if(hit.transform.CompareTag("Bouton"))
            {
                ActiveUI(GrabUI);
                uiActivated = true;

                if(Input.GetKeyDown(KeyCode.E))
                {hit.collider.gameObject.GetComponent<BoutonScript>().Bouton();}
            }
            
            else if(hit.transform.gameObject.tag == "Pcprefabtag")
            {
                ActiveUI(HitUI);
                uiActivated = true;

                if(Input.GetMouseButton(0)){Destroy(hit.collider.gameObject);}
            }

            if(!uiActivated) {DesactivateCurrentUI();}
        }
    }

    void ActiveUI(Image ui)
    {
        if (currentUI != null && currentUI != ui)
        {
            currentUI.enabled = false;
        }

        if (ui != null)
        {
            ui.enabled = true;
            currentUI = ui;
        }
    }

    void DesactivateCurrentUI()
    {
        if (currentUI != null)
        {
            currentUI.enabled = false;
            currentUI = null;
        }
    }
}