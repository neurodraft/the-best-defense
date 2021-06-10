using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{
    public Transform pressurePlate;
    private Vector3 defaultPosition;
    public RemotelyActivatable[] emitters;

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
        Debug.Log("Pressure plate on trigger enter");
        isPressed = true;
        StartCoroutine(StartShooting());
        pressurePlate.position += new Vector3(0, -0.1f, 0);
            
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Pressure plate on trigger exit");
        pressurePlate.position = defaultPosition;
        isPressed = false;
        StopCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {

        while (isPressed)
        {
            foreach (RemotelyActivatable emitter in emitters)
            {
                emitter.ActivateRemotely();
            }
            yield return new WaitForSeconds(1f);
        }
           
    }

}
