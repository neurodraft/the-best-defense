using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothingFactor = 4f;

    public float cameraRotationSpeed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, playerTransform.position, Mathf.Clamp(smoothingFactor * Time.deltaTime, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(new Vector3(0, cameraRotationSpeed * Time.deltaTime, 0));
        } else if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(new Vector3(0, -cameraRotationSpeed * Time.deltaTime, 0));
        }

    }
}
