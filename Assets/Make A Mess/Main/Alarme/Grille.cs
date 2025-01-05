using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grille : MonoBehaviour
{
    [SerializeField] public GameObject _Grille;
    [SerializeField] public GameObject carte;
    public bool card = false;

    public void Card()
    {
        card = true;
        Destroy(carte.gameObject);
    }

    void Update()
    {
        if(card)
        {
            Destroy(_Grille.gameObject);
        }
        else 
        {
            Debug.Log("Va checher la carte d'acces dans le bureau du patron");
        }
    }

    /*public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grab"))
        {

        }
    }*/
}
