using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
