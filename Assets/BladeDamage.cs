using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player");
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 20 } });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player Trigger");
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 20 } });
        }
    }
}
