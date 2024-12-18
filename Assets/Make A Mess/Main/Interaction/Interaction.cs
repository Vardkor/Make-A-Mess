using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interaction : MonoBehaviour
{
    [SerializeField] public GameObject HacheA;
    [SerializeField] public GameObject Hache;
    [SerializeField] public GameObject BombePeintureA;
    [SerializeField] public GameObject BombePeinture;
    [SerializeField] private GameObject extincteurA;
    [SerializeField] private GameObject briquetA;
    [SerializeField] private GameObject briquet;
    [SerializeField] private ParticleSystem extincteurParticles;
    [SerializeField] private ParticleSystem PeintureParticles;
    [SerializeField] private GameObject extincteurPrefab;
    [SerializeField] private GameObject BriquetPrefab;
    [SerializeField] private GameObject HachePrefab;
    [SerializeField] private GameObject BombePeinturePrefab;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Alarme_Incendie alarmeincendie;
    [SerializeField] public dynamite Dynamite;
    [SerializeField] public Alarme_Securite alarme_Securite;
    [SerializeField] public flechettes flechettescript;

    
 



    public Transform GrabMask;  
    public bool HasFlechette;
    public Transform GrabMaskFlechettes; 
    public bool Grabed = false; 
    public bool HasExtincteur = false;
    public bool HasBriquet = false;
    public bool HasHache = false;
    public bool HasDynamite = false;
    public bool HasPeinture = false;
    public bool isUsingExtincteur = false;
    private bool canUseExtincteur = true;
    private bool isUsingPeinture = false;

    private Transform grabbedObject; 
    public float forcelancer = 50f;
    public float TimeExtincteur = 5f;

    //Casser les Objets\\

    public bool BreakObjectYes = false;
    [SerializeField] private BoxCollider boxColliderHache;
    [SerializeField] public Objet objetscript;


    //Bool et game object pour cassage\\

    [SerializeField] GameObject murpascasser;
    [SerializeField] GameObject murcasser;
    BoxCollider bc;

    public bool casser = false;
    public bool pascasser = false;

    void Start()
    {
        extincteurA.SetActive(false);
        briquetA.SetActive(false);
        HacheA.SetActive(false);
        BombePeintureA.SetActive(false);

        boxColliderHache = HacheA.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        
        if (!Grabed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            { 
                if (hit.collider.CompareTag("Grab"))
                {
                    if (Input.GetKeyDown(KeyCode.E)) 
                    {
                        GrabObject(hit.transform);
                    }
                }
                else if (hit.collider.CompareTag("Extincteur"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GrabExtincteur(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.CompareTag("briquet"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        GrabBriquet(hit.collider.gameObject);
                    }    
                }
                else if(hit.collider.CompareTag("hache"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        GrabHache(hit.collider.gameObject);
                    }
                }
                else if(hit.collider.CompareTag("BombePeinture"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        GrabPeinture(hit.collider.gameObject);
                    }
                }
                else if(hit.collider.CompareTag("dynamite"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Grabdynamite(hit.transform);
                    } 
                }
                else if(hit.collider.CompareTag("flechettes"))
                {
                   if(Input.GetKeyDown(KeyCode.E))
                   {
                        GrabFlechettes(hit.transform);
                   } 
                }
                else if(hit.collider.CompareTag("Alarme"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                   {
                        alarme_Securite.ActiveAlarme();
                   }
                }
                else if(hit.collider.CompareTag("Interupteur"))
                {
                    if(!Input.GetKeyDown(KeyCode.E))
                    {

                    }
                }
            }
        }
        else
        { 
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
            
            else if (Input.GetKeyDown(KeyCode.F))
            {
                if(HasDynamite == true)
                {
                    LaunchDynamite();
                }
                else 
                {
                    LaunchObject();
                }
            }

            if(Grabed == true && HasExtincteur == true)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    extincteurParticles.Play();
                    Grabed = true;
                    HasExtincteur = true;
                    isUsingExtincteur = true;
                    StartCoroutine(ExtincteurUsageTimer());
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                extincteurParticles.Stop();
                isUsingExtincteur = false;
            }
            if(HasExtincteur == true && Grabed == true)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    HasExtincteur = false;
                    Grabed = false;
                    extincteurA.SetActive(false);
                    GameObject newExtincteur = Instantiate(extincteurPrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
                }
            }
            else if (Grabed && HasBriquet)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    briquetA.SetActive(false);
                    Grabed = false;
                    HasBriquet = false;
                    GameObject newBriquet = Instantiate(BriquetPrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
                }
            }
            else if (Grabed && HasHache)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HacheA.SetActive(false);
                    Grabed = false;
                    HasHache = false;
                    GameObject newHache = Instantiate(HachePrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
                }
            }
            else if (Grabed && HasPeinture)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BombePeintureA.SetActive(false);
                    Grabed = false;
                    HasPeinture = false;
                    GameObject newBombePeinture = Instantiate(BombePeinturePrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
                }
            }

            if(Grabed == true && HasPeinture == true)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    PeintureParticles.Play();
                    Grabed = true;
                    HasPeinture = true;
                    isUsingPeinture = true;
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                PeintureParticles.Stop();
                isUsingPeinture = false;
            }
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


    //LAUNCH\\
    

    private void LaunchObject()
    {
        if (grabbedObject != null)
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
    }

    private void LaunchDynamite()
    {
        if (grabbedObject != null)
        {
            if(HasDynamite)
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
                Dynamite.DelayDynamite();
            }
        }
    }

    private void Launchflechettes()
    {
        if (grabbedObject != null)
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
            HasFlechette = true;

            if(HasFlechette == true)
            {
                flechettescript.lancer();
            }
        }
    }


    //GRAB\\
    private void GrabBriquet(GameObject briquet)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Grabed && !HasBriquet) 
            {
                briquet.transform.position = new Vector3(-2.03685141f, 3.82893682f, 75.0999985f);
                briquetA.SetActive(true);
                Grabed = true;
                HasBriquet = true;
                alarmeincendie.briquetmain = true;
            }
        }
    }

    private void GrabHache(GameObject Hache)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Grabed && !HasHache) 
            {
                Hache.transform.position = new Vector3(-2.03685141f, 3.82893682f, 75.0999985f);
                HacheA.SetActive(true);
                Grabed = true;
                HasHache = true;
                BreakObjectYes = true;
            }
        }
    }

    

    public void GrabPeinture(GameObject BombePeinture)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Grabed && !HasPeinture) 
            {
                BombePeinture.transform.position = new Vector3(-2.03685141f, 3.82893682f, 75.0999985f);
                BombePeintureA.SetActive(true);
                Grabed = true;
                HasPeinture = true;
            }
        }
    }

    private void GrabObject(Transform objectToGrab)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = GrabMask.position; 
        grabbedObject.SetParent(GrabMask); 
        Grabed = true; 
 
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }

    private void Grabdynamite(Transform objectToGrab)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = GrabMask.position;
        grabbedObject.SetParent(GrabMask); 
        Grabed = true;
        HasDynamite = true;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }


    private void GrabFlechettes(Transform objectToGrab)
    {
        grabbedObject = objectToGrab;
        grabbedObject.position = GrabMaskFlechettes.position; 
        grabbedObject.rotation = Quaternion.Euler(GrabMaskFlechettes.eulerAngles.z, 90f , 0f);

        grabbedObject.SetParent(GrabMaskFlechettes); 
        Grabed = true;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }
    
    private void GrabExtincteur(GameObject extincteur)
    {
        extincteur.transform.position = new Vector3(-2.03685141f,3.82893682f,75.0999985f);
        extincteurA.SetActive(true); 
        Grabed = true; 
        HasExtincteur = true;
    }


    //OTHER\\
    private IEnumerator ExtincteurUsageTimer()
    {
        yield return new WaitForSeconds(TimeExtincteur);

        if (extincteurParticles.isPlaying)
        {
            extincteurParticles.gameObject.SetActive(false);
        }

        isUsingExtincteur = false;
        canUseExtincteur = false;
    }

    public void BreakObject(GameObject Hache)
    {
        Break();
        casser = true;
    }

    private void Awake()
    {
        murpascasser.SetActive(true);
        murcasser.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void Break()
    {
        murpascasser.SetActive(false);
        murcasser.SetActive(true);
        bc.enabled = false;
    }
}