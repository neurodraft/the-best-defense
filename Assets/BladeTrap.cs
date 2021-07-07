using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    public int maxRotationDegree = 45;
    public float bladeSpeed = 1f;

    public Transform rotatingPart;
    private float randomValue;
    // Start is called before the first frame update
    void Start()
    {
        randomValue = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rotatingPart.localRotation = Quaternion.Euler(0f, 0f, maxRotationDegree * Mathf.Sin(randomValue + Time.time * bladeSpeed));
    }
}
