using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flechettes : MonoBehaviour
{
    public bool collisionenter;
    public BoxCollider boxCollider;

    public void lancer()
    {
        if(collisionenter)
        {
            Debug.Log("Ca touche qq choses");
            //Freeze rigidbody
        }
    }

    void OnTriggerEnter(Collider other)
    {
        other = boxCollider;
        collisionenter = true;
    }
}
