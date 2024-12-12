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
    }
}
