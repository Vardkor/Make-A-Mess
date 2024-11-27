using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject HacheA;
    [SerializeField] private GameObject Hache;
    [SerializeField] private GameObject extincteurA;
    [SerializeField] private GameObject briquetA;
    [SerializeField] private GameObject briquet;
    [SerializeField] private ParticleSystem extincteurParticles;
    [SerializeField] private GameObject extincteurPrefab;
    [SerializeField] private GameObject BriquetPrefab;
    [SerializeField] private GameObject HachePrefab;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Alarme_Incendie alarmeincendie;
    public Transform GrabMask;  
    public bool Grabed = false; 
    public bool HasExtincteur = false;
    public bool HasBriquet = false;
    public bool HasHache = false;
    public bool isUsingExtincteur = false;
    private bool canUseExtincteur = true;
    private Transform grabbedObject; 
    public float forcelancer = 50f;
    public float TimeExtincteur = 5f;
    
    //Partie Conduite Voiture \\
    [SerializeField] public CharacterController charactercontrollerperso;
    [SerializeField] public CharacterController charactercontrollervoiture;
    public bool entryvoiture = false; 

    void Start()
    {
        extincteurA.SetActive(false);
        briquetA.SetActive(false);
        HacheA.SetActive(false);
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
                LaunchObject();
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

    
    private void GrabExtincteur(GameObject extincteur)
    {
        extincteur.transform.position = new Vector3(-2.03685141f,3.82893682f,75.0999985f);
        extincteurA.SetActive(true); 
        Grabed = true; 
        HasExtincteur = true;
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

    private IEnumerator ExtincteurUsageTimer()
    {
        yield return new WaitForSeconds(TimeExtincteur);

        if (extincteurParticles.isPlaying)
        {
            extincteurParticles.gameObject.SetActive(false);
            //extincteurParticles.Stop();
        }

        isUsingExtincteur = false;
        canUseExtincteur = false;
    }

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
            }
        }
    }
}