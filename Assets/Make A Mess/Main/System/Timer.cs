using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField]  float remainingTime;

    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        int centseconds = Mathf.FloorToInt(remainingTime % 100); //Ajout de centième de seconde, pas encore testé !!\\
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds, centseconds);
    }
}
