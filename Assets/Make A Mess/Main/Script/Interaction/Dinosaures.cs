using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaures : MonoBehaviour
{
    [SerializeField] public GameObject Dinosaure;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if(hit.collider.CompareTag("Dino"))
            {
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    Dinosaure.SetActive(false);
                }
            }
        }
    }
}
