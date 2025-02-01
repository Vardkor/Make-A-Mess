using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable, ObjetTirrable, Collectible, Vitre};
    public eItemtype itemType;

    public enum eTypeFlame {Inflamable, NoInflamable};
    public eTypeFlame itemflame;

    //Grab\\
    private Transform grabbedObject;
    private Transform launchedObject;
    private Transform trsPlayerGuizmo;
    private Transform trsPlayerSpecial;

    //Float\\
    private float forcelancer = 0f;
    private float forcebreak = 20f;
    private float forcebreaklaunch = 100f;
    private float grabetime = 5.0f;

    private float durationGrabObjectMoov = 0.07f;

    //Bool\\
    [Header("Boolean")]
    public bool Grabed;
    public bool SpecialObject;
    public bool bObjectCassable;
    private bool Launched;
    private bool AttackBreak = false;

    //Section Attack Event\\

    private float attackcooldown = 1f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    //public GameObject hitEffect;

    [Header("Audio Sources")]
    public AudioSource hitSound;
    public AudioSource AttackSwing;
    public AudioSource ThrowItemSound;
    public AudioClip sfxDestruction;
    public AudioSource GrabItemSound;

    [Header("Animation Uniquement pour les attacks")]
    public Animator_Script animatorscript;

    [Header("Autres")]

    //Section Break Object Event\\
    private bool impactDetected = false;
    private bool Isbreak = false;
    public GameObject hitObject;

    //Anim Attack\\
    //private Vector3 rotationAngle = new Vector3(90, 0, 0);
    //private float duration = 0.5f;

    //Section Score\\

    public int CurrentScore = 0;
    public int scorePerObject = 0;
    private bool UpdateScore = false;
    private TextMeshProUGUI scoreObjectScore;
    private bool ScoreManagerGo = false;

    //Temporaire
    public bool CollectibleCollected = false;
    
    //Scale\\
    private Vector3 InitialeScale;

    //Launch Object Clic Long\\

    private float MaxForce = 25f;
    private float ChargeRate = 8f;
    private bool IsCharging = false;




    public void Interact(Transform trsPlayerGuizmo = null, Transform trsPlayerSpecial = null)
    {
        switch(itemType)
        {
            case eItemtype.Extincteur:
            case eItemtype.Briquet:
            case eItemtype.PDB:
            case eItemtype.Hache:
            case eItemtype.Objet:
            case eItemtype.ObjectCassable:
                GrabObject(transform, trsPlayerGuizmo, trsPlayerSpecial);
            break;

            case eItemtype.ObjetTirrable:
                Debug.Log("Je tire");
            break;

            case eItemtype.Collectible:
                DestroyObject();
            break;
        }

    }

    void Start()
    {
        scoreObjectScore = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        canAttack = true;
        Isbreak = false;
        CollectibleCollected = false;

    }
    public void Update()
    {
        if(!Grabed)
        {
            if(Input.GetMouseButton(0))
            {
                if(itemType == eItemtype.Collectible && CollectibleCollected == false)
                {
                    CollectibleCollected = true;
                    DestroyObject();
                }
            }
        }
        

        if(Grabed)
        {   
            if(Input.GetMouseButton(0))
            {
                if(itemType == eItemtype.Extincteur)
                {
                    Debug.Log("OUE");
                }

                if (itemType == eItemtype.PDB && canAttack)
                {
                    AttackCast();
                }
                if(itemType == eItemtype.Briquet)
                {
                    Debug.Log("Oue");
                }
            }

            if(!SpecialObject)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    forcelancer = 5f;
                    IsCharging = true;
                }
                
                if(Input.GetMouseButton(0) && IsCharging)
                {  
                    forcelancer += ChargeRate * Time.deltaTime;
                    forcelancer = Mathf.Clamp(forcelancer, 0f, MaxForce);
                }

                if(Input.GetMouseButtonUp(0) && IsCharging)
                {
                    LaunchObject();
                    IsCharging = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
        }
    }
    private void GrabObject(Transform objectToGrab, Transform trsPlayerGuizmo, Transform trsPlayerSpecial)
    {
        if(SpecialObject == true)
        {
            grabbedObject = objectToGrab;
            Grabed = true;
            
            grabbedObject.SetParent(trsPlayerSpecial);

            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            if (itemType == eItemtype.ObjectCassable)
            {
                bObjectCassable = true;
            }
            if (itemType == eItemtype.PDB)
            {
                SpecialObject = true;
            }

            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerSpecial.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerSpecial.rotation.eulerAngles, durationGrabObjectMoov);
        }
        else if (!SpecialObject)
        {
            grabbedObject = objectToGrab;
            Grabed = true;

            grabbedObject.SetParent(trsPlayerGuizmo);

            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            if (itemType == eItemtype.PDB)
            {
                SpecialObject = true;
            }

            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerGuizmo.rotation.eulerAngles, durationGrabObjectMoov);
        }
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

            /*GrabItemSound.pitch = 0.9f;
            GrabItemSound.Play();*/

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

        launchedObject = grabbedObject;

        ThrowItemSound.Play();

        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;
    }

    //---[Object Specials]---\\

    public void AttackCast()
    {
        canAttack = false;
        Attacking = true;
        AttackRayCast();
    }

    void AttackRayCast()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log(hitObject.name);

            if(hitObject.layer == LayerMask.NameToLayer("BreakableObject"))
            {
                HitTarget(hitObject);
                
                AttackAnimation();
            }
            else
            {
                MissedAttack();
            }
        }
        else
        {
            MissedAttack();
        }
    }

    void MissedAttack()
    {
        AttackSound();
        canAttack = false;
        Attacking = true;
        AttackAnimation();
        Invoke(nameof(ResetAttack), attackcooldown);
    }


    public void HitTarget(GameObject hitObject)
    {
        hitObject.GetComponent<Interactible>().Break();

        hitSound.pitch = 1;
        hitSound.Play();

        /*GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);*/ //POUR LE COUP SUR LE MUR COMME SUR VALO\\
        
        canAttack = false;
        Attacking = true;
        Invoke(nameof(ResetAttack), attackcooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
        Attacking = false;
    }

    //---[SFX de l'attaque]---\\

    void AttackSound()
    {
        if(Attacking == true && canAttack == false)
        {
            AttackSwing.pitch = Random.Range(0.9f, 1.1f);
            AttackSwing.Play();
        }
    }

    //---[Animation de l'attaque]---\\

    void AttackAnimation()
    {
        //animatorscript.startanim = true;
        /*if(Attacking == true)
        {
            if(AnimationAttackPDB !=null)
            {
                AnimationAttackPDB.SetTrigger("TriggerAttack");
                Debug.Log("Anim Attack");
            }
        }*/
    }

    void Break()
    {
        if (bObjectCassable)
        {
            if(!Isbreak)
            {
                if(transform.childCount > 0)
                {
                    Transform child = transform.GetChild(0);

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
                    child.position = transform.position;
                    child.rotation = transform.rotation;
                    
                    Isbreak = true;
                    Score();

                    gameObject.SetActive(false);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Grab") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Vitre"))
        {
            if(itemType == eItemtype.ObjectCassable && bObjectCassable == true && Launched == true)
            {
                if(!Isbreak)
                {
                    Break();
                    if(collision.gameObject.GetComponent<Interactible>()!= null)
                    {
                        collision.gameObject.GetComponent<Interactible>().forcebreak = 2f;
                        collision.gameObject.GetComponent<Interactible>().Break();
                        collision.gameObject.GetComponent<Interactible>().forcebreak = 20f;
                    }
                    else if(itemType == eItemtype.Vitre)
                    {
                        Debug.Log("Vitre casser");
                    }
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

    //---[Score]---\\

    public void Score()
    {
        Camera.main.GetComponent<ScorringManager>().AddScore(scorePerObject);
    }
}