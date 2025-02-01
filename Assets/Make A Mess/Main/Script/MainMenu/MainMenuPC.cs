using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPC : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject NotifUIPC;

    //public GameObject NotifPc;

    public AudioSource OpenPcSFX;
    public AudioSource PhockSFXSong;

    public bool OpenUI=false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        SceneManager.LoadScene("Level");
    }

    public void PhockSong()
    {
        PhockSFXSong.pitch = Random.Range(0.9f,1.1f);
        PhockSFXSong.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
