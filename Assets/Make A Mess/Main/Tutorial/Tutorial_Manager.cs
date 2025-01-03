using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_Manager : MonoBehaviour
{
    [SerializeField] private Light Light;
    [SerializeField] public TextMeshProUGUI textMeshPro;

    public bool Boutton;
    public bool collisionEnter;

    public bool Box1;

    void start()
    {
        Light.intensity = 0f;
        textMeshPro.gameObject.SetActive(false);
    }


    void Update()
    {
        if(collisionEnter && Boutton)
        { 
            Light.intensity = 50f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        collisionEnter = true;
    }

    void OnTriggerExit(Collider other)
    {
        collisionEnter = false;
    }

}
