using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorTrigger : MonoBehaviour
{
    public Transform door;
    private bool isOpen = false;
    private float doorOpeningDuration = 2f;
    public float doorSpeed = 0.2f;
    public AudioSource doorAudioSource;
    
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
        if (other.gameObject.CompareTag("Player"))
        {

            if (!isOpen && other.gameObject.GetComponent<Player>().HasKey())
            {
                StartCoroutine(openDoor());
                EventManager.TriggerEvent("key_used", null);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private IEnumerator openDoor()
    {
        isOpen = true;
        float timer = 0f;
        Vector3 targetPosition = door.position + Vector3.up * 3;
        Vector3 defaultPosition = door.position;

        doorAudioSource.Play();
        while (timer < doorOpeningDuration)
        {
            timer += Time.fixedDeltaTime;
            door.position = Vector3.Lerp(defaultPosition, targetPosition, timer / doorOpeningDuration);
            yield return new WaitForFixedUpdate();
        }
    }
}
