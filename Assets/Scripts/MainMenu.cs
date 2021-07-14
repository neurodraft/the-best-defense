
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    

    public void StartGame()
    {
        EventManager.TriggerEvent("next_level", null);
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
   
}
