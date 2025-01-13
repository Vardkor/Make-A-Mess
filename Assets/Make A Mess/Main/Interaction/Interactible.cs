using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable, ObjetTirrable};
    public eItemtype itemType;

    public enum eTypeFlame {Inflamable};
    public eTypeFlame itemflame;

    //Grab\\
    private Transform grabbedObject;
    private Transform launchedObject;

    //Float\\
    private float forcelancer = 10f;

    //Bool\\

    public bool Grabed;
    public bool SpecialObject;
    public bool bObjectCassable;
    private bool CanBeBreak = false;
    private bool Launched;

    //Section Attack Event\\

    private float attackcooldown = 1f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    /*public GameObject hitEffect;
    public AudioClip hitSound;*/
    private AudioSource AttackSwing;

    //Section Break Object Event\\
    private bool impactDetected = false;
    private bool Isbreak = false;



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
                GrabObject(transform, trsPlayerGuizmo);
            break;

            case eItemtype.ObjetTirrable:
                Debug.Log("Je tire");
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
            if(itemType == eItemtype.Briquet)
            {
                Debug.Log("Oue");
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

        /*Transform child = GetChildOfGrabbedObject();
        if(child != null)
        {
            Debug.Log($"Enfant trouver : {child.name}");
        }*/

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        if(itemType == eItemtype.ObjectCassable)
        {
            bObjectCassable = true;
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

        Launched = true;

        if (itemType == eItemtype.ObjectCassable && bObjectCassable && Launched)
        {
            CanBeBreak = true;
        }

        launchedObject = grabbedObject;

        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;
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
        BreakObject();
        
        /*audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);*/
        
        canAttack = false;
        Attacking = true;
        Invoke(nameof(ResetAttack), attackcooldown);
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

    void BreakObject()
    {
        if (CanBeBreak)
        {
            if (!Isbreak)
            {
                Debug.Log("Cassé");
                Isbreak = true;
                
                Transform child = launchedObject.GetChild(0);
                Debug.Log($"Enfant trouvé : {child.name}");

                //launchedObject.gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Grab"))
        {
            if(itemType == eItemtype.ObjectCassable && bObjectCassable == true && Launched == true && CanBeBreak == true)
            {
                BreakObject();
            }   
        }
    }
}