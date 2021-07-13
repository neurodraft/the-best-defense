﻿
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    

    public void StartGame()
    {
        SceneManager.LoadScene("LevelsScene");
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
   
}
