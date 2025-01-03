using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public bool BoxEnd;
    void OnTriggerEnter(Collider other)
    {
        BoxEnd = true;
    }
}
