using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, Hache, ObjectCassable, ObjetTirrable, Collectible};
    public eItemtype itemType;

    public enum eTypeFlame {Inflamable, NoInflamable};
    public eTypeFlame itemflame;

    //Grab\\
    private Transform grabbedObject;
    private Transform launchedObject;
    private Transform trsPlayerGuizmo;
    private Transform trsPlayerSpecial;

    //Float\\
    private float forcelancer = 5f;
    private float forcelancerClick = 25f;
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
    //public Animator AnimationAttackPDB;

    //public AudioSource GrabItemSound;
    public AudioSource ThrowItemSound;

    public int soundIndex;
    public string ThrowItemSFX;

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
    private ScorringManager scorringManager;
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

        InitialeScale = transform.localScale;

        trsPlayerGuizmo = GameObject.Find("Grab")?.transform;

        if (trsPlayerGuizmo == null){Debug.LogError("trsPlayerGuizmo is not assigned. Ensure the GameObject 'PlayerGuizmo' exists in the scene."); return;}

        //AudioManager.Instance.PlaySoundByIndex(soundIndex = 7);
    }

    public void ResetScale(){transform.localScale = InitialeScale; Debug.Log("Reset Scale");} // Pour reset la scale quand on s'accroupit

    void LateUpdate(){transform.localScale = InitialeScale;} // Reset la scale après avoir lâché l'objet

    public void Update()
    {
        if(Isbreak)
        {
            Score();
        }
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
                    //LaunchObject();
                }
            }

            if(itemType == eItemtype.Collectible && CollectibleCollected == false)
            {
                CollectibleCollected = true;
                DestroyObject();
            }

        }

        if(Grabed)
        {   
            if(Input.GetMouseButtonDown(0))
            {
                IsCharging = true;
                forcelancer = 5f;
            }
            
            if(Input.GetMouseButton(0) && IsCharging)
            {  
                forcelancer += ChargeRate * Time.deltaTime;
                forcelancer = Mathf.Clamp(forcelancer, 0f, MaxForce);
            }

            if((Input.GetMouseButtonUp(0)) && IsCharging)
            {
                LaunchObject();
                IsCharging = false;
            }
        }

        if (Grabed == true && trsPlayerGuizmo != null)
        {
            LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
        }
    }
    private void GrabObject(Transform objectToGrab, Transform trsPlayerGuizmo, Transform trsPlayerSpecial)
    {
        //AudioManager.Instance.PlaySoundByIndex(7);
        if(SpecialObject == true)
        {
            Debug.Log("Oue1");
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
            Debug.Log("Oue");
            ScorringManager scorringManagerInstance = trsPlayerGuizmo.GetComponentInChildren<ScorringManager>();
            if (scorringManagerInstance != null)
            {
                scorringManager = scorringManagerInstance;
                ScoreManagerGo = true;
            }

            grabbedObject = objectToGrab;
            Grabed = true;

            grabbedObject.SetParent(trsPlayerGuizmo);

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

        if (itemType == eItemtype.ObjectCassable && bObjectCassable && Launched)
        {
            CanBeBreak = true;
        }

        launchedObject = grabbedObject;

        ThrowItemSound.Play();

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

            if(hitObject.layer == LayerMask.NameToLayer("BreakableObject"))
            {
                HitTarget(hitObject);
                AttackAnimation();
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
        Isbreak = false;
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
            /*if(AnimationAttackPDB !=null )
            {
                AnimationAttackPDB.SetTrigger("Attack");
            }*/

            Debug.Log("Anim Attack");
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

                    if(Isbreak && ScoreManagerGo)
                    {
                        Score();
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
                            rb.AddForce(launchedObject.transform.forward * forcebreaklaunch, ForceMode.Impulse);
                            rb.isKinematic = false;
                        }
                    }
                    child.SetParent(null);
                    child.position = launchedObject.position;
                    child.rotation = launchedObject.rotation;
                    launchedObject.gameObject.SetActive(false);
                    Isbreak = true;

                    if(Isbreak && ScoreManagerGo)
                    {
                        Score();
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

    //---[Score]---\\

    public void Score()
    {
        //Debug.Log("Score à ajouter : " + scorePerObject);
        scorringManager.AddScore(scorePerObject);
    }
}