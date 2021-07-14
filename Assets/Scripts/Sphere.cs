using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public AudioClip sphereSound;
    private AudioSource audioSource;
    private bool destroyed = false;

    public bool addStamina = false;
    public bool addHealth = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Destroyed()
    {
        destroyed = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(transform.parent.gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if (!destroyed)
        {
            if (other.gameObject.CompareTag("Player") )
            {

                if (sphereSound != null)
                {
                    audioSource.PlayOneShot(sphereSound);

                }

                Player player = other.gameObject.GetComponent<Player>();

                if (addHealth)
                {
                    player.addHealth(10);
                }
                if (addStamina)
                {
                    player.addStamina(10);
                }

                StartCoroutine(Destroyed());

            }
        }
        
    }
}
