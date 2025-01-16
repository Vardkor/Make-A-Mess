using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musique_dans_musee : MonoBehaviour
{
    [SerializeField] public AudioSource speaker;
    //public MusiquePrinciStart MainMusic;

    private bool MusicPlaying = false;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E) && !MusicPlaying)
            {
                if(!MusicPlaying)
                {
                    PlayMusic();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && MusicPlaying)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    if(MusicPlaying)
                    {
                        StopMusic();
                    }
                }
            }
        }
    }

    void PlayMusic()
    {
        speaker.Play();
        MusicPlaying = true;
        //MainMusic.Music.Stop();
    }

    void StopMusic()
    {
        speaker.Stop();
        MusicPlaying = false;
        //MainMusic.Music.Play();
    }
}
