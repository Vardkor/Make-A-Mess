using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float remainingTime = 300;
    [SerializeField] public AudioSource Notif;
    [SerializeField] public AudioSource MainMusic;
    [SerializeField] Objet scriptobjet;
    [SerializeField] BoxCollider boxColliderStart;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject GameWinScreen;

    public bool StartTimer;
    public bool HasPlay;
    public bool PlayerExitMusee = false;
    private bool gameHasEnded = false;

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
        gameHasEnded = false;
    }
    
    void Update()
    {
        if(StartTimer)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
            timer.text = string.Format("{0:00}:{1:00}:{0:00}", minutes, seconds, centiseconds);

            
            if(minutes == 4 && seconds == 30)
            {
                Message1.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 4 && seconds == 0)
            {
                Message2.SetActive(true);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 3 && seconds == 30)
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
            if(minutes == 1 && seconds == 0)
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
            if(minutes == 0 && seconds == 0)
            {
                GameOverEvent();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && gameHasEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void StartingTimer()
    {
        if(StartTimer)
        {
            MainMusic.Play();
        }
    }

    public void TimeExit()
    {
        scriptobjet.TimeExit = true;
    }

    void GameOverEvent()
    {
        if(!PlayerExitMusee)
        { gameHasEnded = true; GameOverScreen.SetActive(true); }
        else { gameHasEnded = true; GameWinScreen.SetActive(true); }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!HasPlay && other.CompareTag("Player"))
        {
            StartTimer = true;
            HasPlay = true;
            StartingTimer(); 
        }
    }
}