using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiquePrinciStart : MonoBehaviour
{
    public AudioSource Music;
    public GameObject ColliderSource;
    
    public void OnTriggerEnter(Collider other)
    {
        Music.Play();
        Destroy(ColliderSource.gameObject);
    }
}
