using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction2 : MonoBehaviour
{
    public Transform trsPlayerGuizmo;
    public GameObject go;

    public GameObject GrabUI;

    public void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 2.5f))
        {
            if(hit.transform.gameObject.tag == "Grab")
            {
                go = hit.transform.gameObject;
                go.GetComponent<Outline>().enabled = true;

                GrabUI.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Interactible>().Interact(trsPlayerGuizmo);
                }

            }
            else
            {
                go.GetComponent<Outline>().enabled = false;
                go = null;

                GrabUI.SetActive(false);
            }
        }
    }
}