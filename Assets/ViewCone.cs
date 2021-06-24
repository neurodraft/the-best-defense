using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{

    public Transform emissionPoint;

    public SimpleEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            emissionPoint.LookAt(new Vector3(other.transform.position.x, emissionPoint.position.y, other.transform.position.z));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            emitter.setIsFiring(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            emitter.setIsFiring(true);
        }
    }
}
