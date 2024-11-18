using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorring : MonoBehaviour
{

    public int Score = 0;
    public GameObject[] objets = new GameObject[10];
    public string Cible;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if(hit.collider.CompareTag("Scrore100"))
            {
                foreach (GameObject obj in objets) 
                {
                    if(obj.name == Cible)
                    {
                        Debug.Log("Ajout de 100 au score");
                        Score =+ 100;
                        Debug.Log("" + Score);
                        break;
                    }
                }
            }
        }
    }
}
