using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public AudioClip keyPickUpSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(keyPickUpSound != null)
            {
                audioSource.PlayOneShot(keyPickUpSound);
            }
            EventManager.TriggerEvent("key_picked_up", null);
            StartCoroutine(Disappear());
            
        }
    }

    private IEnumerator Disappear()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);

    }

}
