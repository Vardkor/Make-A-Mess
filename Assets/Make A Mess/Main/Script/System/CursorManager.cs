using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorHoverTexture;

    public void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(1,1), CursorMode.Auto);
    }

    public void SwitchCursor()
    {
        Cursor.SetCursor(cursorHoverTexture, new Vector2(1,1), CursorMode.Auto);
    }
}
