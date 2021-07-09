using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Transform key;
    private Transform playerTransform = null;
    private Vector3 keyStartLocalPosition;


    // Start is called before the first frame update
    void Start()
    {
        keyStartLocalPosition = key.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (playerTransform == null)
        {
            if (key.localPosition != keyStartLocalPosition)
            {
                key.localPosition = Vector3.Lerp(key.localPosition, keyStartLocalPosition, Time.deltaTime * 2f);
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, key.position.y, playerTransform.position.z);
            key.position = Vector3.Lerp(key.position, targetPosition, Time.deltaTime * 2f);
        }
    }
    void OnTriggerEnter(Collider other)
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
