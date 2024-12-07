using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour
{
    //Script\\

    [SerializeField] Interaction interaction;
    [SerializeField] Alarme_Incendie alarme;


    //GameObject\\

    [SerializeField] private Rigidbody boxColliderHache;
    [SerializeField] private GameObject hache_main;
    [SerializeField] private GameObject peinture;
    [SerializeField] public BoxCollider boxColliderPeinture;
    
    public Vector3 spawnPeinture = Vector3.zero;
    
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

        if(collisionEnter && Input.GetMouseButtonDown(0) && Cassable)  
        {
            interaction.BreakObject(hache);
        }

        if(collisionEnter && Input.GetMouseButtonDown(0) && PeutetrePeint)
        {
            Debug.Log("Ca paint");
            
            Vector3 spawnPosition = boxColliderPeinture.transform.position + spawnPeinture;

            Instantiate(peinture, spawnPosition, Quaternion.identity); 
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
