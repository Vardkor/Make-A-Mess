using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortePatron : MonoBehaviour
{
        [SerializeField] GameObject key;
        [SerializeField] GameObject Door;
        public bool PorteOuverte;
    public void PortePatronfunction()
    {
        if(!PorteOuverte)
        {
            PorteOuverte = true;
            Destroy(Door.gameObject);
        }
    }
}
