using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform playerCamera;

    public float jumpheight = 2f;
    public float gravity = -20f;
    public float speed = 6.5f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;

    public float currentSpeed;

    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;

    public bool isMoving = false;
    public bool Sprinting = false;
    private bool AsJumped = false;
    public bool Crouching = false;
    private bool AsCrouched = false;

    public ParticleSystem jumpPraticles;
    public ParticleSystem ReboundPraticles;
    public AudioSource JumpSFX;
    public AudioSource CrouchSFX;

    private float standingHeight;
    private float crouchingHeight = 1f;
    private Vector3 cameraStandingPosition;
    private Vector3 cameraCrouchOffset = new Vector3(0, -0.5f, 0);

    void Start()
    {
        currentSpeed = speed;
        standingHeight = controller.height;
        cameraStandingPosition = playerCamera.localPosition;
    }


    public void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -8f;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Crouch();
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            currentSpeed = speed;
            Sprinting = false;
        }


        if(Crouching==true)
        {
            currentSpeed = 4f;
        }

        if(Sprinting==true)
        {
            currentSpeed = 15f;
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

        controller.Move(move * currentSpeed * Time.deltaTime);

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

    public void Sprint()
    {
        Sprinting = true;
    }

    public void Crouch()
    {
        if(!Crouching)
        {
            Crouching = true;
            AsCrouched = true;

            controller.height = crouchingHeight;
            cameraTransform.localPosition = cameraStandingPosition + cameraCrouchOffset;

            PlayCrouchSound();
        }
        else
        {
            Crouching = false;

            controller.height = standingHeight;
            cameraTransform.localPosition = cameraStandingPosition;
            PlayCrouchSound(); 
        }

    }

    public void StandUp()
    {
        Crouching = false;
        controller.height = standingHeight;
        playerCamera.localPosition = cameraStandingPosition;
        PlayCrouchSound();
    }

    void PlayCrouchSound()
    {
        if(Crouching==true)
        {
            CrouchSFX.pitch = 1f;
            CrouchSFX.Play();
        }
        if(Crouching==false && AsCrouched == true)
        {
            CrouchSFX.pitch = 0.9f;
            CrouchSFX.Play();
        }
    }
}
