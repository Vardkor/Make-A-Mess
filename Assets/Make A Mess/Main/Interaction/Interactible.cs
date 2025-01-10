using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable};
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
    public bool ObjectCassable;

    //Section Attack Event\\

    private float attackcooldown = 1f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    /*public GameObject hitEffect;
    public AudioClip hitSound;*/
    private AudioSource AttackSwing;


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

            case eItemtype.ObjectCassable:
                GrabObjectBreak(transform, trsPlayerGuizmo);
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
            rb.AddForce(Camera.main.transform.forward * forcelancer, ForceMode.Impulse);
        }
        
        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;

        if(ObjectCassable)
        {
           if(rb.mass == 50f)
            {
                Debug.Log("OuE");
            }
        }
    }

    //---[Object Specials]---\\

    public void AttackItem()
    {
        SpecialObject = true;
        canAttack = false;
        AttackRayCast();
        AttackSound();
        //AttackAnimation();
    }

    void AttackRayCast()
    {

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
        }
        else
        {
            canAttack = true;
            Attacking = false;
            Invoke(nameof(ResetAttack), attackcooldown);
        }
    }
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


    //---[Grab Objet Cassable]---\\
    private void GrabObjectBreak(Transform objectToGrab, Transform trsPlayerGuizmo)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = trsPlayerGuizmo.position;
        grabbedObject.rotation = trsPlayerGuizmo.rotation;
        grabbedObject.SetParent(trsPlayerGuizmo);
        Grabed = true;
        ObjectCassable = true;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    //---[SFX de l'attaque]---\\

    void AttackSound()
    {
        if(Attacking == true)
        {
            AttackSwing = grabbedObject.GetComponent<AudioSource>();
            AttackSwing.pitch = Random.Range(0.9f, 1.1f);
            AttackSwing.Play();
        }
    }

    //---[Animation de l'attaque]---\\

    /*void AttackAnimation()
    {
        if(Attacking == true)
        {
            AnimationAttack = grabbedObject.GetComponent<Animator>();
            AnimationAttack.Play();
        }
        else
        {
            return;
        }
    }*/
}