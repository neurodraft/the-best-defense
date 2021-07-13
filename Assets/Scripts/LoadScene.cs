using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    
   
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAssync(sceneIndex));
    }

    IEnumerator LoadAssync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log("Progresso : " + progress);
            yield return null;
        }
    }
    
}
