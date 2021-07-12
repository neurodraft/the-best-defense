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
        StartCoroutine(Cycle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        StopCoroutine(Cycle());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
        {
            Debug.Log("Pressure plate on trigger enter");
            isPressed = true;
            pressurePlate.position = defaultPosition - new Vector3(0, 0.1f, 0);
        }
        
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
        {
            Debug.Log("Pressure plate on trigger exit");
            pressurePlate.position = defaultPosition;
            isPressed = false;
        }
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            if (isPressed)
            {
                foreach (RemotelyActivatable emitter in emitters)
                {
                    emitter.ActivateRemotely();
                }
            }
            yield return new WaitForSeconds(1f);
        }
        

           
    }

}
