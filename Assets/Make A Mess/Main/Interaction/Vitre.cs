using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitre : MonoBehaviour
{
    [SerializeField] GameObject vitrepascasser;
    [SerializeField] GameObject vitrecasser;
    BoxCollider bc;

    private void Awake()
    {
        vitrepascasser.SetActive(true);
        vitrecasser.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Break();
    }

    private void Break()
    {
        vitrepascasser.SetActive(false);
        vitrecasser.SetActive(true);

        bc.enabled = false;
    }
 
}
