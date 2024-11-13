using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musique_dans_musee : MonoBehaviour
{
    [SerializeField] public AudioSource[] speakers;
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
        foreach (var speaker in speakers)
        {
            speaker.clip = musicClip;
            speaker.Play();
        }
        MusicPlaying = true; 
    }

    void StopMusic()
    {
        foreach (var speaker in speakers)
        {
            speaker.Stop();
        }
        MusicPlaying = false;
    }

}
