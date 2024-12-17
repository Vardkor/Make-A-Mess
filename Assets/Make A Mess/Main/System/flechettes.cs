using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flechettes : MonoBehaviour
{
    public bool collisionenter;
    public BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            boxCollider.isTrigger = false;
        }
    }

    void Update()
    {
        if(collisionenter)
        {
            lancer();
        } 
    }

    public void lancer()
    {
       Debug.Log("Toucher");
    }

    void OnTriggerEnter(Collider other)
    {
        collisionenter = true;
    }
}
