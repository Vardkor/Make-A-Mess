using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bouton : MonoBehaviour
{
    public bool isPressed = false;
    public Rigidbody rb;
    private Vector3 initialPosition;
    public float pressForce = 10f;
    public float resetSpeed = 5f;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if(isPressed)
        {
            Vector3 directionToInitial = (initialPosition - transform.position).normalized;
            rb.AddForce(Vector3.down * pressForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isPressed = true;
        rb.AddForce(Vector3.down * pressForce, ForceMode.Impulse);
    }

    void OnTriggerExit(Collider other)
    {
        isPressed = false;
    }
}
