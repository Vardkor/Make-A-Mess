using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject Menu930;
    [SerializeField] GameObject Menu8;
    [SerializeField] GameObject Menu6;
    [SerializeField] GameObject Menu3;
    [SerializeField] GameObject Menu1;
    
    public void Start()
    {
        Menu930.SetActive(false);
        Menu8.SetActive(false);
        Menu6.SetActive(false);
        Menu3.SetActive(false);
        Menu1.SetActive(false);
    }
    
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
 
        if(minutes == 9 && seconds == 30)
        {
           Menu930.SetActive(true);
        }
        if(minutes == 9 && seconds == 0)
        {
           Menu930.SetActive(false); 
        }
        if(minutes == 8 && seconds == 0)
        {
            Menu8.SetActive(true);
        }
        if(minutes == 7 && seconds == 30)
        {
           Menu8.SetActive(false); 
        }
        if(minutes == 6 && seconds == 0)
        {
            Menu6.SetActive(true);
        }
        if(minutes == 5 && seconds == 30)
        {
           Menu6.SetActive(false); 
        }
        if(minutes == 3 && seconds == 0)
        {
            Menu3.SetActive(true);
        }
        if(minutes == 2 && seconds == 30)
        {
           Menu3.SetActive(false); 
        }
        if(minutes == 1 && seconds == 0)
        {
            Menu1.SetActive(true);
        }
    }
}
