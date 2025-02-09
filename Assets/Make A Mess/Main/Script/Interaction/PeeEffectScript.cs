using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeEffectScript : MonoBehaviour
{
    public ParticleSystem PeeParticle;
    private bool Peeing = false;

    [SerializeField] AudioSource OpenPant;
    [SerializeField] AudioSource PeeSoundFX;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(Peeing==false)
            {
                PeeParticle.Play();
                OpenPant.Play();
                PeeSoundFX.Play();
            }
            else
            {
                Peeing = true;
                PeeParticle.Stop();
            }
        }
    }
}
