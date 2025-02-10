using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] public AudioSource Notif;
    [SerializeField] public AudioSource MainMusic;
    [SerializeField] Objet scriptobjet;
    [SerializeField] BoxCollider boxColliderStart;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject GameWinScreen;

    private float maxTime = 600f;
    public float remainingTime;

    [SerializeField] Slider slidersCops;

    public bool StartTimer;
    public bool HasPlay;
    public bool PlayerExitMusee = false;
    private bool gameHasEnded = false;

    private float duration = 6f;

    [Header("Message On PC")]
    public GameObject Message1;
    public GameObject Message2;
    public GameObject Message3;
    public GameObject Message4;
    public GameObject Message5;
    public GameObject Message6;
    public GameObject Message7;

    [Header("In Game Message")]
    public GameObject IGMessage1;
    public GameObject IGMessage2;
    public GameObject IGMessage3;
    public GameObject IGMessage4;
    public GameObject IGMessage5;
    public GameObject IGMessage6;
    public GameObject IGMessage7;

    private GameObject currentMessage;
    private GameObject currentIGMessage;

    public GameObject NotifUIPC;
    
    public void Start()
    {
        maxTime = remainingTime;
        StartTimer = false;
        gameHasEnded = false;
    }
    
    void Update()
    {
        if(StartTimer)
        {
            remainingTime -= Time.deltaTime;
            UpdateSlider();

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            int centiseconds = Mathf.FloorToInt((remainingTime - Mathf.Floor(remainingTime)) * 100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
            
            if(minutes == 9 && seconds == 50)
            {
                ActiveUI(Message1);
                ActiveInGameUI(IGMessage1);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 4 && seconds == 0)
            {
                ActiveUI(Message2);
                ActiveInGameUI(IGMessage2);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 3 && seconds == 30)
            {
                ActiveUI(Message3);
                ActiveInGameUI(IGMessage3);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 3 && seconds == 0)
            {
                ActiveUI(Message4);
                ActiveInGameUI(IGMessage4);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 1 && seconds == 30)
            {
                ActiveUI(Message5);
                ActiveInGameUI(IGMessage5);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 1 && seconds == 0)
            {
                ActiveUI(Message6);
                ActiveInGameUI(IGMessage6);
                NotifUIPC.SetActive(true);
                Notif.Play();
            }
            if(minutes == 0 && seconds == 45)
            {
                ActiveUI(Message7);
                ActiveInGameUI(IGMessage7);
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

    void UpdateSlider()
    {
        if (slidersCops != null)
        {
            slidersCops.value = remainingTime;
        }
    }

    void ActiveUI(GameObject ui)
    {
        if (currentMessage != null && currentMessage != ui)
        {
            currentMessage.SetActive(false);
        }

        if (ui != null)
        {
            ui.SetActive(true);
            currentMessage = ui;
        }
    }

    void ActiveInGameUI(GameObject IGui)
    {
        if (currentIGMessage != null && currentIGMessage != IGui)
        {
            currentIGMessage.SetActive(false);
        }

        if (IGui != null)
        {
            IGui.SetActive(true);
            CancelInvoke(nameof(DesactivateCurrentUI));
            Invoke(nameof(DesactivateCurrentUI), duration);
            currentIGMessage = IGui;
        }
    }

    void DesactivateCurrentUI()
    {
        if (currentMessage != null)
        {
            currentMessage.SetActive(false);
            currentMessage = null;
        }

        if(currentIGMessage != null)
        {
            currentIGMessage.SetActive(false);
            currentIGMessage = null;
        }
    }
}