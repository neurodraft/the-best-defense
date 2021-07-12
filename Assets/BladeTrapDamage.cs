using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapDamage : MonoBehaviour
{
    public float bladeDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with: " + collision.gameObject.ToString());
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Blade collision player");

            GameObject other = collision.gameObject;

            Vector3 direction = other.gameObject.transform.forward * -1;
            Vector3 position = other.gameObject.transform.position;
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10 }, { "direction", direction }, { "position", position } });
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Blade trigger player");
            Vector3 direction = other.gameObject.transform.forward * -1;
            Vector3 position = other.gameObject.transform.position;
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", bladeDamage }, { "direction", direction }, { "position", position } });
        }
        if (other.CompareTag("Spider"))
        {
            other.gameObject.GetComponent<SpiderAI>().Die();
        }
    }
}
