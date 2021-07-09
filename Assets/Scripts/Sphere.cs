using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public AudioClip sphereSound;
    private AudioSource audioSource;
    private bool destroyed = false;


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
            if (other.gameObject.CompareTag("Player") && other.GetComponent<Player>().getCurrentHealth() < 10)
            {
                if (sphereSound != null)
                {
                    audioSource.PlayOneShot(sphereSound);

                }
                other.GetComponent<Player>().addHealth(1);

                StartCoroutine(Destroyed());

            }
        }
        
    }
}
