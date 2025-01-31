using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrouchBox : MonoBehaviour
{
    public Image CrouchImage;
    
    public void Start()
    {
        CrouchImage.enabled = false; // Désactive l'image au départ
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CrouchImage.enabled = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        CrouchImage.enabled = false;
    }
}
