using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    } 
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volumegame", volume);
    }
    
}
