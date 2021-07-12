using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spikes: " + other.gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.gameObject.transform.forward * -1;
            Vector3 position = other.gameObject.transform.position;
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10f }, { "direction", direction }, { "position", position } });
        }

        if(other.gameObject.CompareTag("Spider"))
        {
            Debug.Log("Spider!");
            other.gameObject.GetComponent<SpiderAI>().Die();
        }
    }
}
