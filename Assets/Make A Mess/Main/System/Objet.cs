using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objet : MonoBehaviour
{
    //Script\\

    [SerializeField] Interaction interaction;
    [SerializeField] Alarme_Incendie alarme;
    [SerializeField] BombePeinture bombepeinture;

    //GameObject\\

    [SerializeField] private GameObject hache_main;
    
    //Bool\\
    public bool collisionEnter;
    public bool Cassable; 
    public bool PeutetreBruler;
    public bool PeutetrePeint;

    void Update()
    {   
        GameObject hache = hache_main.gameObject;
        
        if (collisionEnter)
        {
            if (GetComponent<Collider>().CompareTag("Plante1"))
            {
                Debug.Log("Plante est la ");
            }
        }

        if(collisionEnter && Input.GetMouseButton(0) && Cassable)  
        {
            interaction.BreakObject(hache);
        }

        if(collisionEnter && Input.GetMouseButton(0) && PeutetrePeint)
        { 
            bombepeinture.Peindre();
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
