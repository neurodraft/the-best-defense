﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Key Trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Trigger Key");
            EventManager.TriggerEvent("key_picked_up", null);
            this.gameObject.SetActive(false);
        }
    }

}