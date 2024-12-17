using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitre : MonoBehaviour
{
    [SerializeField] GameObject vitrepascasser;
    [SerializeField] GameObject vitrecasser;
    BoxCollider bc;
    [SerializeField] Scorring scorring;

    public bool casser = false;
    public bool pascasser = false;
    public bool vitrebreak = false;


    private void Awake()
    {
        vitrepascasser.SetActive(true);
        vitrecasser.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Break();
        casser = true;
    }

    private void Break()
    {
        vitrepascasser.SetActive(false);
        vitrecasser.SetActive(true);
        Scorring();
        bc.enabled = false;
        vitrebreak = true;
    }
    
    private void Scorring()
    {
        scorring.CurrentScore += scorring.ScoreVitre;
    }
}    