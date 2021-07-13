
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    
    Scene scene;
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }


}
