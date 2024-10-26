using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorring : MonoBehaviour
{

    public int Score = 0;
    //public static ScoreManager instance;

    void Update()
    {
        Debug.Log("Le score est de : " + Score);
    }
}
