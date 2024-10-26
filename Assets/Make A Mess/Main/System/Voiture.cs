using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conduite_Voiture : MonoBehaviour
{
    [SerializeField] public Interaction interaction;
    public CharacterController controller;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float speed = 12f;
    void Update()
    {
        if (interaction.entryvoiture == true)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            velocity.y += gravity * Time.deltaTime;

            controller.Move(move * speed * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);
        }
    }
}
