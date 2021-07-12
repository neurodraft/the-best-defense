using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderAI : MonoBehaviour
{

    public SpiderSpawner spawner = null;
    private NavMeshAgent agent;
    Animator anim;

    public AudioClip attackSound;
    public AudioClip deathSound;


    private AudioSource audioSource;



    private Vector3 initialPosition;
    private bool attacking = false;
    public float attackCooldown = 8f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (agent.enabled)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
            GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;


            //Debug.Log("Spider speed: " + agent.velocity.magnitude);
            if (attacking)
            {


            }
            else if (agent.velocity.magnitude == 0)
            {

                //Debug.Log("Spider Stopped");
                NavMeshHit hit;
                if (NavMesh.SamplePosition(initialPosition + (new Vector3(-4 + (Random.value * 8), 0, -4 + (Random.value * 8))), out hit, 4f, NavMesh.AllAreas))
                {
                    //Debug.Log("Spider Moving Randomly");
                    agent.destination = hit.position;
                }


            }
        }
        
    }

    public void Push(Vector3 force)
    {
        StartCoroutine(PushCoroutine(force));
    }

    private IEnumerator PushCoroutine(Vector3 force)
    {
        agent.ResetPath();
        agent.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = transform.position + force;
        float timer = 0;
        while(timer < 1)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, timer);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        agent.enabled = true;
    }

    public void Die()
    {
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animator>().SetTrigger("isDead");
        audioSource.PlayOneShot(deathSound);
        StopAllCoroutines();
        StartCoroutine(Disappear());

    }

    private IEnumerator Attack()
    {
        float attackTimer = 0;
        GetComponent<Animator>().SetTrigger("Attack");

        //RaycastHit hit;
        //if(Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit))
        //{
        //    Debug.Log(hit.collider.gameObject);
        //    if (hit.collider.gameObject.CompareTag("Player"))
        //    {
        //        EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10 }, { "direction", transform.forward }, { "position", transform.position + transform.forward} });
        //    }
        //}



        while (attackTimer < attackCooldown)
        {
            attackTimer += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

        }
        attacking = false;
    }

    private IEnumerator Disappear()
    {
        Vector3 start = transform.position;
        Vector3 goal = transform.position + Vector3.down * 0.5f;
        float timer = 0;
        while (transform.position != goal)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            
            transform.position = Vector3.Lerp(start, goal, timer/4f);
        }

        if (spawner != null)
        {
            spawner.SpiderDied(this.gameObject);
        }

        Destroy(this.gameObject);
    }

    public void DealDamage()
    {
        audioSource.PlayOneShot(attackSound);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Player and Shield");

        if (Physics.SphereCast(transform.position + Vector3.up - transform.forward*0.5f, 0.5f, transform.forward, out hit, 2f,mask ))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10f }, { "direction", transform.forward }, { "position", transform.position + transform.forward + Vector3.up } });
            }
            if (hit.collider.gameObject.CompareTag("Shield"))
            {
                hit.collider.gameObject.GetComponent<Shield>().Defended();
            }
        }

        /*if (GetComponentInChildren<SpiderDamageArea>().canDamage())
        {
            EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10 }, { "direction", transform.forward }, { "position", transform.position + transform.forward } });
        }*/
    }
    void OnTriggerStay(Collider other)
    {

        if (agent.enabled == true && other.gameObject.CompareTag("Player"))
        {
            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            //Debug.Log(distance);
            if (distance < 2)
            {
                if (!attacking)
                {
                    agent.ResetPath();
                    attacking = true;
                    StartCoroutine(Attack());
                }
            }
            else
            {
                if (!attacking)
                {

                    agent.destination = other.transform.position;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(other.transform.position + (new Vector3(-4 + (Random.value * 8), 0, -4 + (Random.value * 8))), out hit, 4f, NavMesh.AllAreas))
                    {
                        //Debug.Log("Spider Moving Randomly");
                        agent.destination = hit.position;
                    }

                }
            }
        }

    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }
}
