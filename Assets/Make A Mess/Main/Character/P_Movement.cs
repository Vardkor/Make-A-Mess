using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public CharacterController controller;
    public float jumpheight = 12f;
    public float gravity = -9.81f;
    public float speed = 12f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Ca sprint");
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        

       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       Vector3 move = transform.right * x + transform.forward * z;

       velocity.y += gravity * Time.deltaTime;

       controller.Move(move * speed * Time.deltaTime);

       controller.Move(velocity * Time.deltaTime);
    }
}
