using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPhoneManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject NotifUIPC;

    [SerializeField] public GameObject PcPrefab;
    [SerializeField] Transform SpawnPoint;
    private GameObject currentCube;

    //public GameObject NotifPc;

    public AudioSource OpenPcSFX;
    public AudioSource PhockSFXSong;

    public bool OpenUI=false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(OpenUI==true){Resume();}
            else{Menu();}
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        Cursor.visible = true;

        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        OpenUI = true;

        OpenPcSFX.pitch = 0.9f;
        OpenPcSFX.Play();
        
    }

    public void PhockSong()
    {
        PhockSFXSong.pitch = Random.Range(0.9f,1.1f);
        PhockSFXSong.Play();
    }

    void InstantiatePrefab()
    {
        if(PcPrefab != null && SpawnPoint != null)
        {
            if(currentCube!=null)
            {
                Destroy(currentCube);
            }
            currentCube = Instantiate(PcPrefab, SpawnPoint.position, SpawnPoint.rotation);

            Rigidbody rb = currentCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Camera.main.transform.forward * 15f, ForceMode.Impulse);
            }
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
