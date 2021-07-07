using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public GameObject sphere;
    public Transform transformPosition;
    
    public AudioClip vaseDestroySound;
    private AudioSource audioSource;

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
        if (other.gameObject.CompareTag("Projectile"))
        {
            
            Destroy(gameObject);
            Instantiate(sphere, transformPosition.position + new Vector3(0, 2f, 0), transform.rotation, transform.parent);
            audioSource.PlayOneShot(vaseDestroySound);
        }
    }
}

