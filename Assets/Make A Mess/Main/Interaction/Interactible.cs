using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache};
    public eItemtype itemType;

    //Grab\\
    private Transform grabbedObject;
    

    //Float\\
    private float forcelancer = 50f;

    //Vector\\
    private Vector3 screenPosition;
    private Vector3 cursorPosition;

    //Bool\\

    public bool Grabed;
    public bool SpecialObject;

    //Section PDB\\

    private float attackcooldown = 2f;
    private float attackDistance = 5f;
    private float attackSpeed = 1f;
    public bool canAttack;
    public bool Attacking;
    public LayerMask attackLayer;



    public void Interact(Transform trsPlayerGuizmo = null)
    {
        switch(itemType)
        {
            case eItemtype.Objet:
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.Extincteur:
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.Briquet:
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.PDB:
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.Hache:
                GrabObject(transform, trsPlayerGuizmo);
            break;
        }
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Grabed)
        {
            ReleaseObject();
        }
        
        if(Input.GetMouseButton(0) && Grabed && !SpecialObject)
        {
            if(itemType == eItemtype.Extincteur)
            {
                Debug.Log("OUE");
            }

            if (itemType == eItemtype.PDB)
            {
                PDBInteractible();
            }

            if(!SpecialObject)
            {
                LaunchObject();
            }
        }
    }

    private void GrabObject(Transform objectToGrab, Transform trsPlayerGuizmo)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = trsPlayerGuizmo.position;
        grabbedObject.rotation = trsPlayerGuizmo.rotation;
        grabbedObject.SetParent(trsPlayerGuizmo);
        Grabed = true;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        canAttack = true;
        Attacking = false;
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

        canAttack = false;
        Attacking = false;
        SpecialObject = false ;
    }
    
    private void LaunchObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * forcelancer, ForceMode.Impulse);
        }
        
        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;
    }

    //Object Specials\\

    public void PDBInteractible()
    {
        //if(!canAttack || Attacking) return;
        SpecialObject = true;
        canAttack = false;
        Attacking = true;
        
        Invoke(nameof(ResetAttack), attackSpeed);
        AttackRayCast();
        //Invoke(nameof(AttackRayCast), attackcooldown);
    }

    void AttackRayCast()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
        }
    }
    void ResetAttack()
    {
        Debug.Log("Reset Attack Called");
        canAttack = true;
        Attacking = false;
    }

    void HitTarget(Vector3 pos)
    {
        Debug.Log("Destroy Object");
    }
}