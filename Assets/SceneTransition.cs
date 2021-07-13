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


    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("next_level", LoadNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadNextLevel(Dictionary<string, object> message)
    {
        StartCoroutine(LoadAssync(nextLevelIndex));

    }

    IEnumerator LoadAssync(int sceneIndex)
    {
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
                //m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    operation.allowSceneActivation = true;
            }
        }

  



    }

}
