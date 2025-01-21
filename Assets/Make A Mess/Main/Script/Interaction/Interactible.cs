using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable, ObjetTirrable};
    public eItemtype itemType;

    public enum eTypeFlame {Inflamable, NoInflamable};
    public eTypeFlame itemflame;

    //Grab\\
    private Transform grabbedObject;
    private Transform launchedObject;
    private Transform trsPlayerGuizmo;

    //Float\\
    private float forcelancer = 10f;
    private float grabetime = 5.0f;

    private float durationGrabObjectMoov = 0.07f;

    //Bool\\

    public bool Grabed;
    public bool SpecialObject;
    public bool bObjectCassable;
    private bool CanBeBreak = false;
    private bool Launched;
    private bool AttackBreak = false;

    //Section Attack Event\\

    private float attackcooldown = 1f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    //public GameObject hitEffect;
    public AudioSource hitSound;
    private AudioSource AttackSwing;
    //private Animator AnimationAttack;

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
        Isbreak = false;
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

        if (Grabed == true && trsPlayerGuizmo != null)
        {
            LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
        }
    }

    private void GrabObject(Transform objectToGrab, Transform trsPlayerGuizmo)
    {
        grabbedObject = objectToGrab;
        Grabed = true;

        grabbedObject.SetParent(trsPlayerGuizmo);

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        if(itemType == eItemtype.ObjectCassable)
        {
            bObjectCassable = true;
        }
        if(itemType == eItemtype.PDB)
        {
            SpecialObject = true;
        }

        LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
        LeanTween.rotate(grabbedObject.gameObject, trsPlayerGuizmo.rotation.eulerAngles, durationGrabObjectMoov);

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
            SpecialObject = false;
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
        Attacking = true;
        AttackRayCast();
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
            AttackSound();
            canAttack = false;
            Attacking = true;
            Invoke(nameof(ResetAttack), attackcooldown);
        }
    }
    void ResetAttack()
    {
        canAttack = true;
        Attacking = false;
    }

    public void HitTarget(Vector3 pos)
    {
        CanBeBreak = true;
        AttackBreak = true; 
        BreakObject();

        hitSound.pitch = 1;
        hitSound.Play();

        /*GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);*/
        
        canAttack = false;
        Attacking = true;
        Invoke(nameof(ResetAttack), attackcooldown);
    }

    //---[SFX de l'attaque]---\\

    void AttackSound()
    {
        if(Attacking == true && canAttack == false)
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
            AnimationAttack.Play("PDBAnim");
        }
        else
        {
            if(AnimationAttack != null)
            {
                AnimationAttack = null;
            }
        }
    }*/

    void BreakObject()
    {
        if (CanBeBreak)
        {
            if(!Isbreak)
            {
                if (launchedObject.childCount > 0)
                {
                    if(AttackBreak)
                    {
                       Debug.Log("CA tape en else");
                        Transform child = launchedObject.GetChild(0);

                        List<Transform> allChildren = GetAllChildren(child);

                        foreach (Transform grandChild in allChildren)
                        {
                            Rigidbody rb = grandChild.GetComponent<Rigidbody>();
                            if (rb != null)
                            {
                                rb.isKinematic = false;
                            }
                        }
                        child.SetParent(null);
                        child.position = launchedObject.position;
                        child.rotation = launchedObject.rotation;
                        launchedObject.gameObject.SetActive(false);
                        Isbreak = true;
                    }
                    else
                    {
                        Debug.Log("CA tape en else");
                        Transform child = launchedObject.GetChild(0);

                        List<Transform> allChildren = GetAllChildren(child);

                        foreach (Transform grandChild in allChildren)
                        {
                            Rigidbody rb = grandChild.GetComponent<Rigidbody>();
                            if (rb != null)
                            {
                                rb.isKinematic = false;
                            }
                        }
                        child.SetParent(null);
                        child.position = launchedObject.position;
                        child.rotation = launchedObject.rotation;
                        launchedObject.gameObject.SetActive(false);
                        Isbreak = true;
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Grab"))
        {
            if(itemType == eItemtype.ObjectCassable && bObjectCassable == true && Launched == true && CanBeBreak == true)
            {
                if(!Isbreak)
                {
                    BreakObject();
                }
            }   
        }
    }

    private List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in parent)
        {
            children.Add(child);
            children.AddRange(GetAllChildren(child));
        }

        return children;
    }
}