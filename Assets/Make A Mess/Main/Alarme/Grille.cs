using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grille : MonoBehaviour
{
    [SerializeField] public GameObject _Grille;
    [SerializeField] public GameObject carte;
    
    public bool card = false;
    public AudioSource Key_Sound;

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
            Key_Sound.Play();
        }
    }
}
