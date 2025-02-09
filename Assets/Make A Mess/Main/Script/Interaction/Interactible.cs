using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, ObjectCassable, ObjetTirrable, Collectible, Vitre};
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
    public bool SpecialObject = false;
    public bool bObjectCassable;
    private bool Launched;
    private bool AttackBreak = false;

    //Section Attack Event\\

    private float attackcooldown = 0.5f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    //public GameObject hitEffect;

    [Header("Audio Sources")]
    public AudioSource hitSound;
    public AudioSource AttackSwing;
    public AudioSource ThrowItemSound;
    //public AudioClip sfxDestruction;
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

    public GameObject HitUI;
    public GameObject ThrowUI;

    private bool uiActivated = false;


    public void Interact(Transform trsPlayerGuizmo = null, Transform trsPlayerSpecial = null)
    {
        switch(itemType)
        {
            case eItemtype.Extincteur:
            case eItemtype.Briquet:
            case eItemtype.PDB:
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
        if (!SpecialObject)
        {
            grabbedObject = objectToGrab;
            StartCoroutine(GrabOnTime());
            SpecialObject = false;

            GrabItemSound.pitch = 1f;
            GrabItemSound.Play();

            Vector3 originalScale = grabbedObject.lossyScale;
            grabbedObject.SetParent(trsPlayerGuizmo, true);
            grabbedObject.localScale = originalScale;

            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            if (itemType == eItemtype.PDB)
            {
                SpecialObject = true;
            }
            
            ThrowUI.SetActive(true);
            uiActivated = true;

            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerGuizmo.rotation.eulerAngles, durationGrabObjectMoov);
        }
        else
        {
            grabbedObject = objectToGrab;
            StartCoroutine(GrabOnTime());
            SpecialObject = true;

            GrabItemSound.pitch = 1f;
            GrabItemSound.Play();
            
            Vector3 originalScale = grabbedObject.lossyScale;
            grabbedObject.SetParent(trsPlayerSpecial, true);
            grabbedObject.localScale = originalScale;

            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            if (itemType == eItemtype.ObjectCassable)
            {
                bObjectCassable = true;
            }

            HitUI.SetActive(true);
            uiActivated = true;

            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerSpecial.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerSpecial.rotation.eulerAngles, durationGrabObjectMoov);
        }
    }

    public IEnumerator GrabOnTime()
    {
        yield return new WaitForSeconds(durationGrabObjectMoov);
        Grabed = true;
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

            GrabItemSound.pitch = 0.9f;
            GrabItemSound.Play();

            grabbedObject.SetParent(null);
            grabbedObject = null;
            Grabed = false;

            DesactivateCurrentUI();
            uiActivated = false;
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

        if(ThrowItemSound != null) ThrowItemSound.Play();

        grabbedObject.SetParent(null);
        grabbedObject = null;
        Grabed = false;

        DesactivateCurrentUI();
        uiActivated = false;
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
    Vector3 origin = Camera.main.transform.position;
    Vector3 forward = Camera.main.transform.forward;

    Vector3[] offsets = {
        Vector3.zero,
        Camera.main.transform.right * 0.5f,
        -Camera.main.transform.right * 0.5f,
        Camera.main.transform.up * 0.5f
    };

    bool hasHit = false;

    foreach (Vector3 offset in offsets)
    {
        Vector3 rayOrigin = origin + offset;

        if (Physics.Raycast(rayOrigin, forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            Rigidbody rb = hitObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(forward * forcebreak, ForceMode.Impulse);
            }

            if (hitObject.layer == LayerMask.NameToLayer("BreakableObject"))
            {
                HitTarget(hitObject);
                hasHit = true;
            }

            Debug.DrawRay(rayOrigin, forward * attackDistance, Color.red, 0.2f);
        }
        else
        {
            Debug.DrawRay(rayOrigin, forward * attackDistance, Color.blue, 0.2f);
        }
    }

    if (hasHit)
    {
        AttackAnimation();
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
            if(itemType == eItemtype.ObjectCassable && bObjectCassable && Launched || itemType == eItemtype.Vitre)
            {
                if(!Isbreak)
                {
                    Break();
                    /*if(collision.gameObject.GetComponent<Interactible>()!= null)
                    {
                        collision.gameObject.GetComponent<Interactible>().forcebreak = 2f;
                        collision.gameObject.GetComponent<Interactible>().Break();
                        collision.gameObject.GetComponent<Interactible>().forcebreak = 20f;
                    }*/
                }
            }
        }

        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Vitre"))
        {
            if(itemType == eItemtype.ObjectCassable && bObjectCassable)
            {
                if(GetComponent<Rigidbody>() != null && GetComponent<Rigidbody>().velocity.sqrMagnitude > 0.1f)
                {
                    if(!Isbreak)
                    {
                        Break();
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

    void DesactivateCurrentUI()
    {
        if(HitUI != null || ThrowUI != null)
        {
            HitUI.SetActive(false);
            HitUI = null;
            ThrowUI.SetActive(false);
            ThrowUI = null;
        }
    }
}