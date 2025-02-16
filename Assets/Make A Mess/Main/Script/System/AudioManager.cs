using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource JumpSound;
    public AudioSource PeeSound;
    public AudioSource OpenPantSound;
    public AudioSource HitSound;
    public AudioSource SprintSound;
    public AudioSource ThrowItemSound;
    public AudioSource GrabItemSound;
    public AudioSource CrouchSound;
    public AudioSource ClickSound;
    public AudioSource PhockSound;
    public AudioSource ClingSound;
    public AudioSource Unlock_LockSound;
    public AudioSource AttackSwingSound;
    public AudioSource MainMusic;
    public AudioSource CollectSound;

    void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
