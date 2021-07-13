using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverUI;
    public void Restart()
    {
        SceneManager.LoadScene("LevelsScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
