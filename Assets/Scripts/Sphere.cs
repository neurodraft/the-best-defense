using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<Player>().getCurrentHealth() < 10)
        {
            
            Destroy(gameObject);
            
            other.GetComponent<Player>().addHealth(1);
            Destroy(GetComponent<HealthPickup>().gameObject);


        }
    }
}
