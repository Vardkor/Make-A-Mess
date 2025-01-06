using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonDino : MonoBehaviour
{
    public GameObject DinoManager;
    public AudioSource Explosion;

    void OnTriggerEnter(Collider other)
    {
        Destroy(DinoManager.gameObject);
        Explosion.Play();
    }
}
