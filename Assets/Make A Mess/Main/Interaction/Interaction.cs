using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject extincteurA; 
    [SerializeField] private ParticleSystem extincteurParticles;
    public Transform GrabMask;  
    public bool Grabed = false; 
    public bool HasExtincteur = false;
    private Transform grabbedObject; 
    public float forcelancer = 50f; 
    

    void Start()
    {
        extincteurA.SetActive(false); 
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
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    if (!extincteurParticles.isPlaying)
                    {
                        extincteurParticles.Play();
                        Grabed = true;
                        HasExtincteur = true;
                    }
                    else if (extincteurParticles.isPlaying)
                    {
                        extincteurParticles.Stop();
                    }
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
        Destroy(extincteur); 
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

    void ShootExtincteur()
    {
        if(Grabed == true && HasExtincteur == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Objet Attraper en main");
            }
        }
    }
}
