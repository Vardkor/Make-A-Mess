using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extincteur : MonoBehaviour
{
    [SerializeField] public GameObject extincteurA;

    public bool grabed = false;
    void Start()
    {
        extincteurA.SetActive(false);
    }


    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (hit.collider.CompareTag("Extincteur"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    grabed = true;
                    Destroy(hit.collider.gameObject);
                    extincteurA.SetActive(true); 
                }   
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                
            }

        }
        
    }
}
