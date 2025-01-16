using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interupteurs : MonoBehaviour
{
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
    }
}
