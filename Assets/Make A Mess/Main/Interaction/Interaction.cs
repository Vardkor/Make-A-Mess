using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interaction : MonoBehaviour
{
    [SerializeField] public GameObject HacheA;
    [SerializeField] public GameObject Hache;
    [SerializeField] public GameObject PDBA;
    [SerializeField] public GameObject PDB;
    [SerializeField] private GameObject PDBPrefab;
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
    [SerializeField] public Alarme_Securite alarmesecurite;
    [SerializeField] public PortePatron portepatronscript;
    [SerializeField] public Grille _G_rille;

    
 



    public Transform GrabMask;  
    public bool HasFlechette;
    public Transform GrabMaskFlechettes; 
    public bool Grabed = false; 
    public bool HasExtincteur = false;
    public bool HasBriquet = false;
    public bool HasHache = false;
    public bool HasDynamite = false;
    public bool HasPeinture = false;
    public bool HasPDB = false;
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

    public Camera cam;
    public LayerMask paintableLayer;

    public AudioSource KeySound;

    void Start()
    {
        extincteurA.SetActive(false);
        briquetA.SetActive(false);
        HacheA.SetActive(false);
        BombePeintureA.SetActive(false);
        PDBA.SetActive(false);

        boxColliderHache = HacheA.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        
        if (!Grabed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
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
                else if(hit.collider.CompareTag("PDB"))
                {
                    /*PDB.GetComponent<Outline>().enabled = true;
                    /*else
                    {
                        PDB.GetComponent<Outline>().enabled = false;
                    }*/

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        GrabPDB(hit.collider.gameObject);
                    }
                }
                else if(hit.collider.CompareTag("Key"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        portepatronscript.PortePatronfunction();
                        Destroy(hit.collider.gameObject);
                        KeySound.Play();
                    }
                }
                else if(hit.collider.CompareTag("carteacces"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        _G_rille.Card();
                        Destroy(hit.collider.gameObject);
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
            else if (Grabed && HasPeinture)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BombePeintureA.SetActive(false);
                    Grabed = false;
                    HasPeinture = false;
                    GameObject newBombePeinture = Instantiate(BombePeinturePrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
                    Peinturing();
                }
            }
            else if (Grabed && HasPDB)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PDBA.SetActive(false);
                    Grabed = false;
                    HasPDB = false;
                    GameObject newBombePeinture = Instantiate(PDBPrefab, transform.position + transform.forward * 1.0f, Quaternion.identity);
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
        grabbedObject.rotation = GrabMaskFlechettes.rotation;

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

    private void GrabPDB(GameObject PDB)
    {
        PDB.transform.position = new Vector3(-2.03685141f,3.82893682f,75.0999985f);
        PDBA.SetActive(true); 
        Grabed = true; 
        HasPDB = true;
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

    public void BreakObjectHache(GameObject Hache)
    {
        Break();
        casser = true;
    }
    public void BreakObjectPDB(GameObject PDB)
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
    
    void Peinturing()
    {
        if (Input.GetMouseButton(0)) // Clic gauche
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 5f, paintableLayer))
            {
                TexturePainter painter = hit.collider.GetComponent<TexturePainter>();
                if (painter != null)
                {
                    painter.Paint(hit.textureCoord);
                }
            }
        }
    }

}