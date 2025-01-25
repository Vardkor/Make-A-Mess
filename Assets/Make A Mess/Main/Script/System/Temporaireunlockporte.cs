using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporaireunlockporte : MonoBehaviour
{
    public Interactible InteractibleScript;

    void Update()
    {
        if(InteractibleScript.CollectibleCollected==true)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
