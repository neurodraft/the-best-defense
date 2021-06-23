using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    public float lifeTime = 10f;

    private bool disappearing = false;

    private float timer = 0;

    public ParticleSystem particleSystem;
    public ParticleSystem destroyedParticleSystem;
    public Light light;

    private bool toBeDestroyed = false;
    public AudioClip wallSound;
    public AudioClip shieldImpact;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!disappearing && timer >= lifeTime)
        {
            disappear();
        }
    
    }

    void FixedUpdate()
    {
        /*
        if(!disappearing && rb.velocity.magnitude < 0.001f)
        {
            //Debug.Log(rb.velocity.magnitude.ToString("#.00000000"));
            disappear();
        }*/
    }

    IEnumerator DestroyAfterParticles()
    {
        toBeDestroyed = true;
        Debug.Log("Particle Sytem Play");
        light.enabled = false;
        particleSystem.Play();
        destroyedParticleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(gameObject);
    }

    public void disappear()
    {
        timer = 0;
        disappearing = true;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(DestroyAfterParticles());
    }

    public void ImmediateDisappear()
    {

    }

    public void Boost(float charge)
    {
        if (!disappearing)
        {
            timer = 0;
            StartCoroutine(ApplyBoostNextUpdate(charge));
        }
    }

    IEnumerator ApplyBoostNextUpdate(float charge)
    {
        yield return new WaitForFixedUpdate();
        rb.velocity = rb.velocity.normalized * 30*charge;
    }


    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield")){
            audioSource.PlayOneShot(shieldImpact);
        }
        else
        {
            audioSource.PlayOneShot(wallSound);
        }
        //  && collision.impulse.magnitude > 0.1f
        if (particleSystem != null)
        {
            Debug.Log("Particle Sytem Play Impact");

            particleSystem.Play();
        }


        if (!disappearing && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile")))
        {

            Debug.Log(collision.collider.gameObject.name);

            if (collision.gameObject.CompareTag("Player"))
            {
                EventManager.TriggerEvent("player_damage", new Dictionary<string, object> { { "amount", 10 }, { "contact_point", collision.GetContact(0) } });
            }
            
            disappear();
        }
    }

}
