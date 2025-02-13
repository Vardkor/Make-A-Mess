using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Camera : MonoBehaviour
{
    public float mouseSensity = 100f;

    public Transform playerBody; 

    float xRotation = 0f;

    public P_Movement PlayerMovement;

    public Camera playerCamera;

    public Transform cameraTransform;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.3f;

    public AudioSource SprintSFX;

    private float m_FieldOfView;

    public Transform CrouchPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_FieldOfView = 70f;
    }

    
    void Update()
    {
        playerCamera.fieldOfView = m_FieldOfView;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(PlayerMovement.Sprinting==true && PlayerMovement.isMoving==true)
        {
            m_FieldOfView = Mathf.Lerp(m_FieldOfView, 80, 10f * Time.deltaTime);
            SprintSFX.pitch = 0.9f;
            SprintSFX.Play();
        }
        else
        {
            m_FieldOfView = Mathf.Lerp(m_FieldOfView, 70, 10f * Time.deltaTime);
        }
    }

    public void SetMouse(float newSensitivity)
    {
        mouseSensity = newSensitivity;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-0.5f, 0.5f) * shakeMagnitude;
            float y = Random.Range(-0.5f, 0.5f) * shakeMagnitude;

            cameraTransform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
}
