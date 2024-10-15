using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Camera : MonoBehaviour
{
    public float mouseSensity = 100f;

    public Transform playerBody; 

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
