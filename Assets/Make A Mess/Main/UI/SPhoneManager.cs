using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPhoneManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    //[SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject MailMenu;

    //public GameObject NotifPc;

    public AudioSource OpenPcSFX;
    //public AudioSource PhockSFXSong;

    private bool OpenUI=false;

    void Start()
    {
        //SettingsMenu.SetActive(false);
        MailMenu.SetActive(false);

        PauseMenu.SetActive(false);
        //NotifPc.SetActive(false);
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
        PauseMenu.SetActive(false);
        OpenUI = false;

        OpenPcSFX.pitch = 1.1f;
        OpenPcSFX.Play();

        //NotifPc.SetActive(false);
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
        OpenUI = true;

        OpenPcSFX.pitch = 0.9f;
        OpenPcSFX.Play();
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

    public void Quit()
    {
        Application.Quit();
    }
}
