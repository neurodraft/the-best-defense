using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseHealth : MonoBehaviour
{
    public GameObject sphere;
    public Transform transformPosition;

    public AudioClip vaseDestroySound;
    private AudioSource audioSource;

    private bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Destroyed()
    {
        destroyed = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyed)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                Instantiate(sphere, transformPosition.position + Vector3.up, transform.rotation, transform.parent);
                if (vaseDestroySound != null)
                {
                    audioSource.PlayOneShot(vaseDestroySound);

                }
                StartCoroutine(Destroyed());
            }
        }

    }
}

