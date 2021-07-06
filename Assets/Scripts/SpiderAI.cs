using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderAI : MonoBehaviour
{

    public SpiderSpawner spawner = null;
    private NavMeshAgent agent;


    private Vector3 initialPosition;


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        Debug.Log("Spider speed: " + agent.velocity.magnitude);
        if (agent.velocity.magnitude == 0)
        {

            Debug.Log("Spider Stopped");
            NavMeshHit hit;
            if (NavMesh.SamplePosition(initialPosition + (new Vector3(-4 + (Random.value * 8), 0, -4 + (Random.value * 8))), out hit, 4f, NavMesh.AllAreas))
            {
                Debug.Log("Spider Moving Randomly");
                agent.destination = hit.position;
            }


        }
    }

    public void Die()
    {
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animator>().SetTrigger("isDead");
        if(spawner != null){
            spawner.SpiderDied(this.gameObject);
        }

        StartCoroutine(Disappear());

    }

    private IEnumerator Disappear(){
        Vector3 start = transform.position;
        Vector3 goal = transform.position + Vector3.down * 0.5f;
        float timer = 0;
        while(transform.position != goal){
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start, goal, timer);
        }

        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            agent.destination = other.transform.position;
        }

    }
}
