using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;
    [SerializeField] GameObject MenuPause;

    void Start ()
    {
        MenuPause.SetActive(false);
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        Debug.Log("Resume button pressed");
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Menu()
    {
        MenuPause.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
