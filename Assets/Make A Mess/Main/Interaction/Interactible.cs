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

    //Section Attack Event\\

    private float attackcooldown = 1f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    /*public GameObject hitEffect;
    public AudioClip ItemSwing;
    public AudioClip hitSound;*/


    //DEBUG\\




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

    void Start()
    {
        canAttack = true;
    }


    public void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && Grabed)
        {
            ReleaseObject();
        }
        
        if(Input.GetMouseButton(0) && Grabed)
        {
            if(itemType == eItemtype.Extincteur)
            {
                Debug.Log("OUE");
            }

            if (itemType == eItemtype.PDB && canAttack)
            {
                AttackItem();
                //SetAnimations();
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
            rb.AddForce(transform.forward * forcelancer, ForceMode.Impulse);
        }
        
        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;
    }

    //Object Specials\\

    public void AttackItem()
    {
        SpecialObject = true;
        canAttack = false;
        AttackRayCast();
        
        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.PlayOneShot(AttackSwing);
    }

    void AttackRayCast()
    {
        Vector3 start = transform.position;

        Vector3 end = transform.position + transform.forward * attackDistance;

        Debug.DrawLine(start, end, Color.red, 1f);

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
        }
    }   
        //gameObject.GetComponent<Interaction2>().AttackRaycastCam();
    void ResetAttack()
    {
        Debug.Log("Reset Attack Called");
        canAttack = true;
        Attacking = false;
    }

    public void HitTarget(Vector3 pos)
    {
        Debug.Log("Destroy Object");
        
        /*audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);*/
        
        canAttack = false;
        Attacking = true;
        Invoke(nameof(ResetAttack), attackcooldown);
    }
}