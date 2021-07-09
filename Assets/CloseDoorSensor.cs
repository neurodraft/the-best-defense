using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorSensor : MonoBehaviour
{
    public Transform door;
    //private bool isOpen = true;
    private float doorOpeningDuration = 5f;
    public float doorSpeed = 0.2f;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = door.position + Vector3.down * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        }
    }

    /*private IEnumerator closeDoor()
    {
        isOpen = true;
        float timer = 0f;
        Vector3 targetPosition = door.position + Vector3.up * 3;
        Vector3 defaultPosition = door.position;


        while (timer < doorOpeningDuration)
        {
            timer += Time.fixedDeltaTime;
            door.position = Vector3.Lerp(defaultPosition, targetPosition, timer * doorSpeed);
            yield return null;
        }
    }*/
}
