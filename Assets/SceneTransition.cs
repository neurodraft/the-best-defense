using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public int nextLevelIndex;
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject text;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        EventManager.StartListening("next_level", LoadNextLevel);
        EventManager.StartListening("restart_level", RestartLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadNextLevel(Dictionary<string, object> message)
    {
        StartCoroutine(LoadAssync(nextLevelIndex));

    }

    private void RestartLevel(Dictionary<string, object> message)
    {
        StartCoroutine(LoadAssync(SceneManager.GetActiveScene().buildIndex));

    }

    IEnumerator LoadAssync(int sceneIndex)
    {
        Time.timeScale = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log("Progresso : " + progress);
            yield return null;

            // Check if the load has finished
            if (operation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                text.SetActive(true);
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    operation.allowSceneActivation = true;
            }
 
        }
        Time.timeScale = 1f;








    }

}
