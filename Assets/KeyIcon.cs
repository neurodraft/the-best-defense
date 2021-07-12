using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIcon : MonoBehaviour
{
    public GameObject keyIcon;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("key_picked_up", ShowKeyIcon);
        EventManager.StartListening("key_used", HideKeyIcon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowKeyIcon(Dictionary<String, object> message)
    {
        Debug.Log("Key picked up UI");
        keyIcon.SetActive(true);
    }

    private void HideKeyIcon(Dictionary<String, object> message)
    {
        Debug.Log("Key used UI");
        keyIcon.SetActive(false);
    }
}
