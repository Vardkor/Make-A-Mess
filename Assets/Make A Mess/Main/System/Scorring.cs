using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorring : MonoBehaviour
{

    public int CurrentScore = 0;
    public int ScoreVitre = 100;

    void Update()
    {
        Debug.Log("Score :" + CurrentScore);
    }
}
