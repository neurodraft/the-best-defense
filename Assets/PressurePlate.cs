using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform pressurePlate;
    private Vector3 defaultPosition;

    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = pressurePlate.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Debug.Log("Pressure plate on trigger enter");
            EventManager.TriggerEvent("fire_projectile", null);
            pressurePlate.position = new Vector3(pressurePlate.position.x, 0.01f, pressurePlate.position.z);
            isPressed = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            Debug.Log("Pressure plate on trigger exit");
            pressurePlate.position = defaultPosition;
            isPressed = false;
        }
        
        
    }

}
