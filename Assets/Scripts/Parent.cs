using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public float amplifier = 1;

    private float counter = 0.0f;

    public GameObject camera;
    void Start()
    {
        Child c = this.transform.GetComponentInChildren<Child>();
        //c.SetAmplification(amplifier);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        counter += Time.deltaTime;
        camera.transform.position = new Vector3(camera.transform.position.x,
                                                Mathf.Sin(counter),
                                                camera.transform.position.z);
                                                */
    }

    void FixedUpdate()
    {
        
    }
}
