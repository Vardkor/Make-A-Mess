using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musique_dans_musee : MonoBehaviour
{
    [SerializeField] public AudioSource Speakers1;
    [SerializeField] public AudioSource Speakers2;
    [SerializeField] public AudioSource Speakers3;
    [SerializeField] public AudioSource Speakers4;

    [SerializeField] public AudioClip musicClip;

    private bool MusicPlaying = false;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E) && !MusicPlaying)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    PlayMusic();
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && MusicPlaying)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    StopMusic();
                }
            }
        }
    }

    void PlayMusic()
    {
        Speakers1.Play();
        Speakers2.Play();
        Speakers3.Play();
        Speakers4.Play();
        MusicPlaying = true; 
    }

    void StopMusic()
    {
        Speakers1.Stop();
        Speakers2.Stop();
        Speakers3.Stop();
        Speakers4.Stop();
        MusicPlaying = false; 
    }

}
