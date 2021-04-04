using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{

    private GameObject parent; 

    private float counter = 0.0f;
    private float amplifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        counter += Time.deltaTime;

        Vector3 newPosition = parent.transform.position;

        newPosition += Vector3.up;

        newPosition.x += Mathf.Sin(counter)*amplifier;

        this.transform.position = newPosition;
    }

    public void SetAmplification(float a){
        amplifier = a;
    }
}
