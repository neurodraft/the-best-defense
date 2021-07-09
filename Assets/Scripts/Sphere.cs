using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public AudioClip sphereSound;
    private AudioSource audioSource;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<Player>().getCurrentHealth() < 10)
        {
            
            other.GetComponent<Player>().addHealth(1);
            Destroy(gameObject);
            Destroy(GetComponent<HealthPickup>().gameObject);
           




        }
    }
}
