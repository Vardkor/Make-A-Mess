using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamite : MonoBehaviour
{
    [SerializeField] private float explosionForce = 1000f;  // Force de l'explosion
    [SerializeField] private float explosionRadius = 5f;   // Rayon d'effet
    [SerializeField] private float upwardsModifier = 1f;   // Souffle vers le haut

    //Sound\\

    [SerializeField] public AudioClip musicClip;
    [SerializeField] public AudioSource[] speakers;
    // Delay\\

    public float delay = 3f;
    public float countdown;

    //Bool\\
    
    public bool timer;
    public bool hasexplosed;

    public void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasexplosed)
        {
            Explode();
        }
    }
    //public void DelayDynamite()
    //{
        //countdown = delay;
        //timer = true;
        //Debug.Log("Dynamite");
    //}



    public void Explode()
    {
        if(timer == true)
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
}
