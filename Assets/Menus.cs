
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject keyIcon;
    public GameObject howToPLayMenuUI;

    private void Start()
    {
        EventManager.StartListening("player_died", GameOver);
        EventManager.StartListening("key_picked_up", KeyPickedUp);
        EventManager.StartListening("key_used", KeyUsed);
        
    }

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
        SceneManager.LoadScene("Scenes/MainMenu");
    } 
    
    public void RestartButton()
    {
        gameOverMenuUI.SetActive(false);

        EventManager.TriggerEvent("restart_level", null);

        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void GameOver(Dictionary<string, object> message)
    {
        Time.timeScale = 0f;
        gameOverMenuUI.SetActive(true);
    }

    public void KeyPickedUp(Dictionary<string, object> message)
    {
        keyIcon.SetActive(true);
    }

    public void KeyUsed(Dictionary<string, object> message)
    {
        keyIcon.SetActive(false);
    }
    public void OptionsButton()
    {
        Time.timeScale = 0f;
        howToPLayMenuUI.SetActive(true);
    }
    public void BackButton()
    {
        Time.timeScale = 1f;
        howToPLayMenuUI.SetActive(false);
    }



}
