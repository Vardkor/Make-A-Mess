using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchBox : MonoBehaviour
{
    public GameObject ImageToSee;
    
    public void Start()
    {
        ImageToSee.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ImageToSee.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        ImageToSee.SetActive(false);
    }
}
