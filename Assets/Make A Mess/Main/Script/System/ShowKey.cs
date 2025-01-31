using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    public GameObject ShowImageToKey;

    void Start(){ShowImageToKey.SetActive(false);}

    void OnTriggerEnter(Collider other){ShowImageToKey.SetActive(true);}

    void OnTriggerExit(Collider other){ShowImageToKey.SetActive(false);}
}
