using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float jumpheight = 12f;
    public float gravity = -9.81f;
    public float speed = 6.5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;

    public bool isMoving = false;
    public bool Sprinting = false;
    private bool AsJumped = false;

    public ParticleSystem jumpPraticles;
    public ParticleSystem ReboundPraticles;
    public AudioSource JumpSFX;


    public void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 12f;
            Sprinting = true;
        }
        else
        {
            speed = 6.5f;
            Sprinting = false;
        }


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
            JumpSFX.pitch = Random.Range(0.9f, 1.1f);
            jumpPraticles.Play();
            JumpSFX.Play();
            AsJumped = true;
        }
        if(AsJumped && isGrounded==false)
        {
            ReboundPraticles.Play();
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * speed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
        
        if(z<=0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
