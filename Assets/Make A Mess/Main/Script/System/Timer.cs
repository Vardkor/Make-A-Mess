using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float remainingTime;
    [SerializeField] public AudioSource Notif;
    [SerializeField] public AudioSource soundTimer;
    [SerializeField] Objet scriptobjet;
    public bool StartTimer;

    public GameObject Message1;
    public GameObject Message2;
    public GameObject Message3;
    public GameObject Message4;
    public GameObject Message5;
    public GameObject Message6;
    public GameObject Message7;
    public GameObject NotifUIPC;
    
    public void Start()
    {
        StartTimer = false;
    }
    
    void Update()
    {
        if(StartTimer)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            //int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds/*, centiseconds*/);

            
            if(minutes == 9 && seconds == 30)
            {
                Message1.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 8 && seconds == 0)
            {
                Message2.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 6 && seconds == 0)
            {
                Message3.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 3 && seconds == 0)
            {
                Message4.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 1 && seconds == 30)
            {
                Message5.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 1 && seconds == 00)
            {
                Message6.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 0 && seconds == 45)
            {
                Message7.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
        }
    }

    public void StartingTimer()
    {
        StartTimer = true;
        soundTimer.Play();
    }

    public void TimeExit()
    {
        scriptobjet.TimeExit = true;
    }

    void OnTriggerEnter(Collider other)
    {
        StartTimer = true;
    }
}