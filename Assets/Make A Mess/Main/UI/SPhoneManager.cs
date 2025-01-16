using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPhoneManager : MonoBehaviour
{
    [SerializeField] GameObject SphoneUI;
    public GameObject NotifUISPhone;
    public AudioSource OpenSPhoneSFX;
    private bool OpenUI=false;

    void Start()
    {
        SphoneUI.SetActive(false);
        NotifUISPhone.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(OpenUI==true)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    void Open()
    {
        SphoneUI.SetActive(true);
        OpenUI = true;
        OpenSPhoneSFX.pitch = 1.1f;
        OpenSPhoneSFX.Play();
        NotifUISPhone.SetActive(false);
    }

    void Close()
    {
        SphoneUI.SetActive(false);
        OpenUI = false;
        OpenSPhoneSFX.pitch = 0.9f;
        OpenSPhoneSFX.Play();
    }
}
