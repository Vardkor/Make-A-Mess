using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable, ObjetTirrable, PcPortablePrefab};
    public eItemtype itemType;

    public enum eTypeFlame {Inflamable, NoInflamable};
    public eTypeFlame itemflame;

    //Grab\\
    private Transform grabbedObject;
    private Transform launchedObject;
    private Transform trsPlayerGuizmo;

    //Float\\
    private float forcelancer = 10f;
    private float forcebreak = 20f;
    private float forcebreaklaunch = 50f;
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
    private Animator AnimationAttack;

    //Section Break Object Event\\
    private bool impactDetected = false;
    private bool Isbreak = false;
    GameObject hitObject;

    //Anim Attack\\
    //private Vector3 rotationAngle = new Vector3(90, 0, 0);
    //private float duration = 0.5f;



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

            case eItemtype.PcPortablePrefab:
                DestroyObject();
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
        
        if(Input.GetMouseButton(0))
        {
            if(Grabed)
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

            if(itemType == eItemtype.PcPortablePrefab)
            {
                DestroyObject();
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

    private void DestroyObject()
    {
        Destroy(this.gameObject);
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
            GameObject hitObject = hit.collider.gameObject;

            if(hitObject.layer == LayerMask.NameToLayer("Object"))
            {
                HitTarget(hitObject);
                AttackAnimation();
            }
            else 
            {
                ResetAttack();
                AttackAnimation();
            }
        }
        else
        {
            AttackSound();
            canAttack = false;
            Attacking = true;
            AttackAnimation();
            Invoke(nameof(ResetAttack), attackcooldown);
        }
    }
    void ResetAttack()
    {
        canAttack = true;
        Attacking = false;
    }
    public void HitTarget(GameObject hitObject)
    {
        CanBeBreak = true;
        AttackBreak = true;

        BreakObject(hitObject);

        hitSound.pitch = 1;
        hitSound.Play();

        /*GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);*/ //POUR LE COUP SUR LE MUR COMME SUR VALO\\
        
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

    void AttackAnimation()
    {
        if(Attacking == true)
        {
            Debug.Log("Attack");
            /*Quaternion initialRotation = transform.rotation;


            LeanTween.rotate(transform.gameObject, transform.rotation.eulerAngles + rotationAngle, duration)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() =>
                {
                    LeanTween.rotate(transform.gameObject, initialRotation.eulerAngles, duration)
                    .setEase(LeanTweenType.easeInOutSine);
                });*/
        }
    }

    void BreakObject(GameObject hitObject)
    {
        if (CanBeBreak)
        {
            if(!Isbreak || AttackBreak)
            {
                if(hitObject.transform.childCount > 0)
                {
                    Transform child = hitObject.transform.GetChild(0);

                    List<Transform> allChildren = GetAllChildren(child);

                    foreach (Transform grandChild in allChildren)
                    {
                        Rigidbody rb = grandChild.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                            rb.AddForce(Camera.main.transform.forward * forcebreak, ForceMode.Impulse);
                        }
                    }
                    child.SetParent(null);
                    child.position = hitObject.transform.position;
                    child.rotation = hitObject.transform.rotation;
                    hitObject.gameObject.SetActive(false);
                    Isbreak = true;
                    AttackBreak = false;

                    if(Isbreak)
                    {
                        ResetAttack();
                    }
                }    
                else if (launchedObject.childCount > 0)
                {
                    Transform child = launchedObject.GetChild(0);

                    List<Transform> allChildren = GetAllChildren(child);

                    foreach (Transform grandChild in allChildren)
                    {
                        Rigidbody rb = grandChild.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddForce(launchedObject.transform.forward * forcebreaklaunch, ForceMode.Acceleration);
                            rb.isKinematic = false;
                        }
                    }
                    child.SetParent(null);
                    child.position = launchedObject.position;
                    child.rotation = launchedObject.rotation;
                    launchedObject.gameObject.SetActive(false);
                    Isbreak = true;

                    if(Isbreak)
                    {
                        ResetAttack();
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Grab") || collision.gameObject.CompareTag("Player"))
        {
            if(itemType == eItemtype.ObjectCassable && bObjectCassable == true && Launched == true && CanBeBreak == true)
            {
                if(!Isbreak)
                {
                    hitObject = collision.gameObject;
                    BreakObject(hitObject);
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