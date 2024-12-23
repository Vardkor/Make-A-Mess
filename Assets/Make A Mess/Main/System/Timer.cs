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
        int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);

        
        if(minutes == 9.30)
        {
            Debug.Log("9.30  MINUTES");
        }
        if(minutes == 8)
        {
            Debug.Log("8  MINUTES");
        }
        if(minutes == 6)
        {
            Debug.Log("6  MINUTES");
        }
        if(minutes == 3)
        {
            Debug.Log("3  MINUTES");
        }
        if(minutes == 1)
        {
            Debug.Log("1  MINUTES");
        }
    }
}
