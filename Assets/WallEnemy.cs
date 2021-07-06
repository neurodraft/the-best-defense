using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
    public Transform headPivot;
    private Transform playerTransform = null;

    private Vector3 lookingAt;

    public Transform emissionPoint;

    public GameObject projectilePrefab;

    private float timer = 0;

    public float interval = 1.0f;

    public float shootForce = 10f;

    public MeshRenderer eye;

    // Start is called before the first frame update
    void Start()
    {
        lookingAt = emissionPoint.position + Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //eye.material.
    }

    private void FixedUpdate()
    {
        if(playerTransform != null)
        {
            
            Vector3 goal = new Vector3(playerTransform.position.x, this.transform.position.y, playerTransform.position.z);
            lookingAt = Vector3.Lerp(lookingAt, goal, Time.deltaTime*4);
            headPivot.LookAt(lookingAt);

            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0;
                Fire();
            }
        }
    }


    private void Fire()
    {
        if (emissionPoint != null && projectilePrefab != null)
        {
            GameObject instance = Instantiate(projectilePrefab, emissionPoint.position, emissionPoint.rotation, transform.parent);

            instance.GetComponent<Rigidbody>().AddForce(emissionPoint.forward * shootForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered.");
            playerTransform = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited.");

            timer = 0;
            playerTransform = null;
        }
    }
}
