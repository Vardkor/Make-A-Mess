using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] GameObject FinUI;
    public bool BoxEnd;
    void OnTriggerEnter(Collider other)
    {
        BoxEnd = true;
        //FinUI.SetActive(false);
    }

    void Start()
    {
        FinUI.SetActive(false);
    }
}
