using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{
    public Transform pressurePlate;
    private Vector3 defaultPosition;
    public RemotelyActivatable[] emitters;
    private float timer = 0f;

    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = pressurePlate.position;
        //StartCoroutine(Cycle());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        pressurePlate.position = defaultPosition;
    }

    //private void OnDestroy()
    //{
    //    StopCoroutine(Cycle());
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
    //    {
    //        Debug.Log("Pressure plate on trigger enter");
    //        isPressed = true;
    //        pressurePlate.position = defaultPosition - new Vector3(0, 0.1f, 0);
    //    }
        
            
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
    //    {
    //        Debug.Log("Pressure plate on trigger exit");
    //        pressurePlate.position = defaultPosition;
    //        isPressed = false;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
        {
            pressurePlate.position = defaultPosition - new Vector3(0, 0.1f, 0);
            
            if(timer > 1f)
            {
                foreach (RemotelyActivatable emitter in emitters)
                {
                    emitter.ActivateRemotely();
                }
                timer = 0;
            }
            
        }
    }

    //private IEnumerator Cycle()
    //{
    //    while (true)
    //    {
    //        if (isPressed)
    //        {
    //            foreach (RemotelyActivatable emitter in emitters)
    //            {
    //                emitter.ActivateRemotely();
    //            }
    //        }
    //        yield return new WaitForSeconds(1f);
    //    }
        

           
    //}

}
