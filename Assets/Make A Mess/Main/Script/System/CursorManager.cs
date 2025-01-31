using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public GameObject _CursorManager;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorHoverTexture;
    
    [SerializeField] private SPhoneManager PauseMenu;
    public AudioSource ClicSong;


    public void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(PauseMenu.OpenUI == true)
            {
                _CursorManager.SetActive(true);
                ClicSong.pitch = Random.Range(0.9f, 1.1f);
                ClicSong.Play();
            }
        }
    }
    /*void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PauseMenu.OpenUI == true)
        {
            _CursorManager.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else{Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;}
    }*/

    public void SwitchCursor(){Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);}
}
