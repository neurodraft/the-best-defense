
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenuUI;

    public void StartGame()
    {
        EventManager.TriggerEvent("next_level", null);
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
   public void OptionsButton()
    {
        optionsMenuUI.SetActive(true);

    }
    public void BackButton()
    {
        optionsMenuUI.SetActive(false);

    }
}
