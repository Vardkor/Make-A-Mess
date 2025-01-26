using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public CharacterController controller;

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

    private Vector3 crouchScale = new Vector3(1, 0.2f, 1);
    private Vector3 PlayerScale = new Vector3(1, 0.653995f, 1);

    private Interactible ObjectGrabbed;
    private Vector3 grabbedObjectOriginalScale;

    void Start()
    {
        currentSpeed = speed;
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
            if(ObjectGrabbed != null){ObjectGrabbed.ResetScale();}
            SetObjectGrabbed();
            Crouch();
            transform.localScale = crouchScale;
            transform.position = new Vector3 (transform.position.x, transform.position.y -0.5f,transform.position.z) * Time.deltaTime;
            PlayCrouchSound();
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            if(ObjectGrabbed != null){ObjectGrabbed.ResetScale();}
            SetObjectGrabbed();
            transform.localScale = PlayerScale;
            transform.position = new Vector3 (transform.position.x, transform.position.y +0.5f,transform.position.z) * Time.deltaTime;
            currentSpeed = speed;
            Crouching = false;
            PlayCrouchSound();
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
        Crouching = true;
        AsCrouched = true;
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

    void SetObjectGrabbed()
    {
        if(ObjectGrabbed != null){GameObject.Find("Grab").GetComponent<Interactible>();}
        else{ObjectGrabbed = null;}
    }
}
