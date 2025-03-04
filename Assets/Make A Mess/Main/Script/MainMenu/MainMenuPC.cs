using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPC : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject NotifUIPC;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Slider LoadingBar;

    public AudioSource OpenPcSFX;
    public AudioSource PhockSFXSong;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadingScreen.SetActive(false);
    }

    public void Resume()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        LoadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync("Level");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
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