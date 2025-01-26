using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    public AudioSource[] audioSources;
    private int currentAudioSource = 0;

    public AudioClip[] audioClip;

    public float globalVolume = 1f;


    void Awake()
    {
        if(Instance == null){DontDestroyOnLoad(gameObject);}
        else{Destroy(gameObject);}

        audioSources = new AudioSource[1];
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
        }
    }

    public void PlaySoundByIndex(int index, float volume = 1f)
    {
        if (index >= 0 && index < audioClip.Length)
        {
            PlaySound(audioClip[index], volume);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        audioSources[currentAudioSource].clip = clip;
        audioSources[currentAudioSource].volume = volume * globalVolume;
        audioSources[currentAudioSource].Play();

        currentAudioSource = (currentAudioSource + 1) % audioSources.Length;
    }

    public void PlayMusic(AudioClip musicClip, float volume = 0.5f)
    {
        AudioSource musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();
    }

    /*public void StopMusic()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying && GameIsFinished || GameOver)
            {
                source.Stop();
            }
        }
    }*/
}
