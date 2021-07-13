using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour
{
    public Transform door;
    private bool isOpen = false;
    private float doorOpeningDuration = 5f;
    public float doorSpeed = 0.2f;
    private Vector3 targetPosition;
    public AudioSource doorAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = door.position + Vector3.up * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Trigger Enter");
            if (!isOpen)
            {
                
                Debug.Log("Is Not Open");
                StartCoroutine(openDoor());
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
            door.position = Vector3.Lerp(defaultPosition, targetPosition, timer * doorSpeed);
            yield return null;
        }   
    }


}
