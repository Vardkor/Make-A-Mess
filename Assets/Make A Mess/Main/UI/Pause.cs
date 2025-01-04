using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool IsPaused = false;
    [SerializeField] GameObject MenuPause;

    void Start ()
    {
        MenuPause.SetActive(false);
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
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
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        MenuPause.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
