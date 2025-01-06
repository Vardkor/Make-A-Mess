using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonDino : MonoBehaviour
{
    public GameObject DinoManager;
    public AudioSource Explosion;
    public float detectionDistance = 3f;

    private bool DinoExplode = false;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!DinoExplode)
                {
                    Destroy(DinoManager);
                    Explosion.Play();
                    DinoExplode = true;
                    Debug.Log("Dino détruit !");
                }
                else if (hit.collider.CompareTag("Dino"))
                {
                    Explosion.Stop();
                    Debug.Log("Explosion stoppée !");
                }
            }
        }
    }
    /*public GameObject DinoManager;
    public AudioSource Explosion;

    private bool DinoExplode = false;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.E) && !DinoExplode)
            {
                if(!DinoExplode)
                {
                    Destroy(DinoManager.gameObject);
                    Explosion.Play();
                    DinoExplode = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && DinoExplode)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    if(DinoExplode)
                    {
                        Explosion.Stop();
                    }
                }
            }
        }
    }*/
}
