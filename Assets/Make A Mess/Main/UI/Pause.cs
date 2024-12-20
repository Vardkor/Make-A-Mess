using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;
    [SerializeField] GameObject MenuPause;


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

    void Resume()
    {
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Menu()
    {
        MenuPause.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
}
