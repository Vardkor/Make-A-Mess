using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet};
    public eItemtype itemType;

    //Grab\\
    private Transform grabbedObject;
    public bool Grabed = false;

    public void Interact(Transform trsPlayerGuizmo = null)
    {
        switch(itemType)
        {
            case eItemtype.Objet:
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.Extincteur:
                Debug.Log("Attraper extincteur");
            break;

            case eItemtype.Briquet:
                Debug.Log("Attraper Briquet");
            break;
        }
    }


    private void GrabObject(Transform objectToGrab, Transform trsPlayerGuizmo)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = trsPlayerGuizmo.position;
        grabbedObject.SetParent(trsPlayerGuizmo);
        Grabed = true; 

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}