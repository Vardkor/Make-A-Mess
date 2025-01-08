using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet};
    public eItemtype itemType;

    //Grab\\
    private Transform grabbedObject;
    public bool Grabed;

    //Float\\
    private float forcelancer = 50f;

    //Vector\\
    private Vector3 screenPosition;
    private Vector3 cursorPosition;

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


    public void Update()
    {
        /*screenPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }*/

        if(Input.GetKeyDown(KeyCode.E) && Grabed)
        {
            ReleaseObject();
        }
        
        if(Input.GetMouseButton(0) && Grabed /*&& !SpecialItem*/)
        {
            LaunchObject();
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

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            grabbedObject.SetParent(null); 
            grabbedObject = null; 
            Grabed = false; 
        }
    }
    
    private void LaunchObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(cursorPosition * forcelancer, ForceMode.Impulse);
        }
        

        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;
    }
}