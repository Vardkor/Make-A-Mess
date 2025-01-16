using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamite : MonoBehaviour
{
    [SerializeField] private float explosionForce = 100f;  // Force de l'explosion
    [SerializeField] private float explosionRadius = 2f;   // Rayon d'effet
    [SerializeField] private float upwardsModifier = 0.2f;   // Souffle vers le haut

    //Sound\\

    [SerializeField] public AudioClip musicClip;
    [SerializeField] public AudioSource[] speakers;

    // Delay\\

    public float delay = 3f;
    public float countdown;

    //Bool\\
    
    public bool timer;
    public bool timerend;
    public bool hasexplosed;

    //Script\\

    [SerializeField] public Interaction interaction;





    public void Update()
    {
        if(timer == true)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0f && !hasexplosed)
            {
                Explode();
                timerend = true;
            }
        }
    }
    public void DelayDynamite()
    {
        if(interaction.HasDynamite == true)
        {
            timer = true;
            PlayMusic();
            interaction.HasDynamite = false;
        }

    }

    void Start()
    {
        countdown = delay;
    }


    public void Explode()
    {
        if(timerend == true)
        {
            
            hasexplosed = true;
            Vector3 explositionPosition = transform.position;

            Collider[] colliders = Physics.OverlapSphere(explositionPosition, explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, explositionPosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
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
    }
}
