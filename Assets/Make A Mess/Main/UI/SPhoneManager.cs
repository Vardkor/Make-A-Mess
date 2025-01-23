using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPhoneManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    //[SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject MailMenu;
    [SerializeField] GameObject NotifUIPC;

    [SerializeField] public GameObject PcPrefab;
    [SerializeField] Transform SpawnPoint;
    private GameObject currentCube;

    //public GameObject NotifPc;

    public AudioSource OpenPcSFX;
    //public AudioSource PhockSFXSong;

    private bool OpenUI=false;

    void Start()
    {
        //SettingsMenu.SetActive(false);
        MailMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(OpenUI==true)
            {
                Resume();
            }
            else
            {
                Menu();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        OpenUI = false;

        OpenPcSFX.pitch = 1.1f;
        OpenPcSFX.Play();

        InstantiatePrefab();
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        OpenUI = true;

        OpenPcSFX.pitch = 0.9f;
        OpenPcSFX.Play();

        NotifUIPC.SetActive(false);
        
    }

    /*public void SettingsActivation()
    {
        SettingsMenu.SetActive(true);
    }

    public void SettingsFalse()
    {
        SettingsMenu.SetActive(false);
    }*/

    public void MailActivation()
    {
        MailMenu.SetActive(true);
    }

    public void MailFalse()
    {
        MailMenu.SetActive(false);
    }

    /*public void PhockSong()
    {

    }*/

    void InstantiatePrefab()
    {
        if(PcPrefab != null && SpawnPoint != null)
        {
            if(currentCube!=null)
            {
                Destroy(currentCube);
            }
            currentCube = Instantiate(PcPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }
        else
        {
            PcPrefab = null;
        }
    }

    void DestroyCurrentPrefab()
    {
        if(currentCube!=null)
        {
            Destroy(currentCube);
            currentCube = null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
