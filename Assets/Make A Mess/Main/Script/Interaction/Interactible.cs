using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Interactible : MonoBehaviour
{
    public enum eItemtype { Objet, Extincteur, Briquet, PDB, ObjectCassable, ObjetTirrable, Collectible, Vitre, EndDoor};
    public eItemtype itemType;

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
    public bool ForFlame = false;
    public bool bObjectCassable;
    private bool Launched;
    private bool AttackBreak = false;

    [Header("Fire")]

    public GameObject firePrefab;
    public bool isBurning = false;

    public int poolSize = 10;
    private Queue<GameObject> firePool = new Queue<GameObject>();



    //Section Attack Event\\
    private float attackcooldown = 0.5f;
    private float attackDistance = 5f;
    public bool canAttack = true;
    public bool Attacking = false;
    public LayerMask attackLayer;

    //public GameObject hitEffect;

    [Header("Autres")]

    //Section Break Object Event\\
    private bool impactDetected = false;
    private bool Isbreak = false;
    public GameObject hitObject;

    //Section Score\\

    public int CurrentScore = 0;
    public int scorePerObject = 0;
    private bool UpdateScore = false;
    private TextMeshProUGUI scoreObjectScore;
    private bool ScoreManagerGo = false;
    
    //Scale\\
    private Vector3 InitialeScale;

    //Launch Object Clic Long\\

    private float MaxForce = 25f;
    private float ChargeRate = 17f;
    private bool IsCharging = false;

    private bool uiActivated = false;

    private Slider sliderLancer;

    public ParticleSystem CollectItemParticles;
    public bool ExitMusee = false;

    //public AudioSource DestructionSFX;


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

            case eItemtype.EndDoor:
                EndGame();
            break;
        }

    }

    void Start()
    {
        scoreObjectScore = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        GameObject sliderObj = GameObject.Find("SliderLancer");
        if (sliderObj != null)
        {
            sliderLancer = sliderObj.GetComponent<Slider>();
            sliderLancer.gameObject.SetActive(false);
            sliderLancer.value = 0;
        }

        canAttack = true;
        Isbreak = false;
        Grabed = false;

        AudioManager audio = AudioManager.Instance;

        // Initialisation du pool FIRE 
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fire = Instantiate(firePrefab);
            fire.SetActive(false);
            firePool.Enqueue(fire);
        }
    }

    public void Update()
    {
        if(!Grabed)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(itemType == eItemtype.Collectible)
                {
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
                    AttackBriquet();
                }
            }

            if(!SpecialObject)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    forcelancer = 5f;
                    IsCharging = true;
                    sliderLancer.gameObject.SetActive(true);
                    Debug.Log("Slider activé");
                    UpdateSlider();
                }
                
                if(Input.GetMouseButton(0) && IsCharging)
                {  
                    forcelancer += ChargeRate * Time.deltaTime;
                    forcelancer = Mathf.Clamp(forcelancer, 0f, MaxForce);
                    UpdateSlider();
                }

                if(Input.GetMouseButtonUp(0) && IsCharging)
                {
                    LaunchObject();
                    IsCharging = false;
                    sliderLancer.gameObject.SetActive(false);
                    UpdateSlider();
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
            Grabed = true;
            grabbedObject = objectToGrab;
            StartCoroutine(GrabOnTime());
            SpecialObject = false;

            AudioManager.Instance.GrabItemSound.pitch = 1f;
            AudioManager.Instance.GrabItemSound.Play();

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


            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerGuizmo.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerGuizmo.rotation.eulerAngles, durationGrabObjectMoov);
        }
        else
        {
            Grabed = true;
            grabbedObject = objectToGrab;
            StartCoroutine(GrabOnTime());
            SpecialObject = true;

            AudioManager.Instance.GrabItemSound.pitch = 1f;
            AudioManager.Instance.GrabItemSound.Play();
            
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

            // Animation de l'objet
            LeanTween.move(grabbedObject.gameObject, trsPlayerSpecial.position, durationGrabObjectMoov);
            LeanTween.rotate(grabbedObject.gameObject, trsPlayerSpecial.rotation.eulerAngles, durationGrabObjectMoov);
        }
    }

    public IEnumerator GrabOnTime()
    {
        Grabed = true;
        yield return new WaitForSeconds(durationGrabObjectMoov);
    }
    
    private void DestroyObject()
    {
        if(CollectItemParticles != null)
        {
            CollectItemParticles.Play();
        }
        AudioManager.Instance.GrabItemSound.pitch = 0.9f;
        AudioManager.Instance.GrabItemSound.Play();
        Destroy(gameObject);
    }

    public void EndGame()
    {
        ExitMusee = true;
    }

    public void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            AudioManager.Instance.GrabItemSound.pitch = 0.9f;
            AudioManager.Instance.GrabItemSound.Play();

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

        launchedObject = grabbedObject;

        AudioManager.Instance.ThrowItemSound.Play();

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

        AudioManager.Instance.HitSound.pitch = Random.Range(0.9f,1.1f);
        AudioManager.Instance.HitSound.Play();

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
            AudioManager.Instance.AttackSwingSound.pitch = Random.Range(0.9f,1.1f);
            AudioManager.Instance.AttackSwingSound.Play();
        }
    }

    //---[Animation de l'attaque]---\\

    void AttackAnimation()
    {
        Attacking = true;

        Vector3 startRotation = new Vector3(0, 0, 0);
        Vector3 endRotation = new Vector3(20, 60, 25);

        transform.localRotation = Quaternion.Euler(startRotation);

        LeanTween.rotateLocal(gameObject, endRotation, 0.1f)
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.rotateLocal(gameObject, startRotation, 0.3f)
                    .setEaseInQuad()
                    .setOnComplete(() => Attacking = false);
            });
    }

    void Break()
    {
        if (bObjectCassable)
        {
            if(!Isbreak)
            {
                if(transform.childCount > 0)
                {
                    //DestructionSFX.pitch = Random.Range(0.9f,1.1f);
                    //DestructionSFX.Play();

                    FindObjectOfType<P_Camera>().StartShake();
                    FindObjectOfType<DestructionPriceManagers>().AddNewPrice(scorePerObject);
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
                    //DestructionSFX.pitch = Random.Range(0.9f,1.1f);
                    //DestructionSFX.Play();
                    FindObjectOfType<P_Camera>().StartShake();
                    Break();
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
                        //DestructionSFX.pitch = Random.Range(0.9f,1.1f);
                        //DestructionSFX.Play();
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

    private void UpdateSlider()
    {
        if (sliderLancer != null)
        {
            if(IsCharging)
            {
                sliderLancer.gameObject.SetActive(true);
                sliderLancer.value = forcelancer;
            }
            else{sliderLancer.gameObject.SetActive(false);}
        }
    }


    public void AttackBriquet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Interactible interactible = hit.collider.GetComponent<Interactible>();

            if (interactible != null && interactible.ForFlame && !interactible.isBurning)
            {
                interactible.isBurning = true;
                interactible.StartFire(hit.point, interactible.transform); // Passe la position du Raycast et l'objet parent
            }
        }
    }


    public void StartFire(Vector3 hitPosition, Transform parentTransform)
    {
        GameObject fireInstance = FirePoolManager.Instance.GetFire();
        
        if (fireInstance != null)
        {
            fireInstance.transform.position = hitPosition; // Position exacte du Raycast
            fireInstance.transform.rotation = Quaternion.identity;
            fireInstance.transform.SetParent(parentTransform); // Devient un enfant de l'objet touché
            fireInstance.transform.localScale = Vector3.zero; // Démarre à une taille nulle

            StartCoroutine(GrowFire(fireInstance)); // Lance l'animation d'agrandissement
            StartCoroutine(ExtinguishFire(fireInstance)); // Commence le compte à rebours pour éteindre
            StartCoroutine(SpreadFire(parentTransform)); // Lance la propagation après un délai
        }
    }




    public void StopFire(GameObject fire)
    {
        fire.SetActive(false);
        firePool.Enqueue(fire);
    }

    private IEnumerator SpreadFire(Transform originObject)
    {
        yield return new WaitForSeconds(3f);

        Collider[] nearbyObjects = Physics.OverlapSphere(originObject.position, 3f); // RAYON DE RECHERCHE POUR LA PROPAGATION DU FEUX
        foreach (Collider col in nearbyObjects)
        {
            Interactible interactible = col.GetComponent<Interactible>();
            if (interactible != null && !interactible.isBurning && interactible.ForFlame)
            {
                interactible.isBurning = true;
                interactible.StartFire(interactible.transform.position, interactible.transform);
            }
        }
    }


    private IEnumerator ExtinguishFire(GameObject fire)
    {
        float duration = 13f;
        float elapsed = 0f;
        Vector3 initialScale = fire.transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scaleMultiplier = Mathf.Lerp(1f, 0f, elapsed / duration);
            fire.transform.localScale = initialScale * scaleMultiplier;
            yield return null;
        }

        StopFire(fire);
    }

   private IEnumerator GrowFire(GameObject fire)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = new Vector3(30f, 30f, 30f);

        fire.transform.localScale = initialScale;
        fire.SetActive(true); 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            fire.transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            yield return null;
        }

        fire.transform.localScale = finalScale; 
    }


}