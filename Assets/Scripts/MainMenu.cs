﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    
}
