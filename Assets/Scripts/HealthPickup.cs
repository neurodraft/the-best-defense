using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Transform sphere;
    private Transform playerTransform=null;
    private Vector3 sphereStartLocalPosition;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        sphereStartLocalPosition = sphere.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (sphere != null) {
            if (playerTransform == null)
            {

                if (sphere.localPosition != sphereStartLocalPosition)
                {

                    sphere.localPosition = Vector3.Lerp(sphere.localPosition, sphereStartLocalPosition, Time.deltaTime * 2f);
                }
            }
            else
            {
                Vector3 targetPosition = new Vector3(playerTransform.position.x, sphere.position.y, playerTransform.position.z);
                sphere.position = Vector3.Lerp(sphere.position, targetPosition, Time.deltaTime * 2f);

            }
        }


        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTransform = other.gameObject.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            playerTransform = null;
        }
    }
    
}
