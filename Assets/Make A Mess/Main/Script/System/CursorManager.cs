using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorHoverTexture;
    
    //[SerializeField] private SPhoneManager PauseMenu;
    //public AudioSource ClicSong;


    public void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    /*void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(PauseMenu.OpenUI == true)
            {
                ClicSong.Play();
            }
        }
    }*/

    public void SwitchCursor()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }
}
