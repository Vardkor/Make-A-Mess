using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clim : MonoBehaviour
{
    [SerializeField] public AudioClip musicClip;
    [SerializeField] public AudioSource[] speakers;
    [SerializeField] private float volume = 1f;
    private bool MusicPlaying;

    void Start()
    {
        PlayMusic();
    }


    public void SoundUp()
    {
        foreach (var speaker in speakers)
        {
            speaker.volume = volume;
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
}
