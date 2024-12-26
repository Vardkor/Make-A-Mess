using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour
{
    [SerializeField] public Camera surveillanceCamera;
    [SerializeField] public RenderTexture cameraRender;

    void Start()
    {
        surveillanceCamera.targetTexture = cameraRender;
    }
}
