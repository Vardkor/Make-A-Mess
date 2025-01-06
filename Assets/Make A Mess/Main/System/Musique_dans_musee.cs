using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musique_dans_musee : MonoBehaviour
{
    [SerializeField] public AudioSource speaker;
    public MusiquePrinciStart MainMusic;

    private bool MusicPlaying = false;
    private bool Collision = false;

    /*void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E) && !MusicPlaying)
            {

            }
            else if (Input.GetKeyDown(KeyCode.E) && MusicPlaying)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    
                }
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if(!MusicPlaying)
        {
            PlayMusic();
            Debug.Log("Oui");
            MainMusic.Music.Stop();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(MusicPlaying)
        {
            StopMusic();
            Debug.Log("Non");
        }
    }

    void PlayMusic()
    {
        speaker.Play();
        MusicPlaying = true; 
    }

    void StopMusic()
    {
        speaker.Stop();
        MusicPlaying = false;
    }
}
