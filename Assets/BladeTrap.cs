using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    public int maxRotationDegree = 45;
    public float bladeSpeed = 1f;
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
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotationDegree * Mathf.Sin(Time.time * bladeSpeed));
    }
}
