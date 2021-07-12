using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
    public Transform headPivot;
    private Transform playerTransform = null;

    private Vector3 lookingAt;

    public Transform emissionPoint;
    public MeshRenderer eyeRenderer;
    private Color eyeEmissionColor;

    public GameObject projectilePrefab;

    private float timer = 0;

    public float interval = 1.0f;

    public float shootForce = 10f;

    public float rotationLimit = 0f;

    private Vector3 previousPlayerPosition;
    private bool trackingPlayer = false;

    private Animator anim;

    private bool retracted = false;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        lookingAt = emissionPoint.position + Vector3.forward;
        eyeEmissionColor = eyeRenderer.material.GetColor("_EmissionColor");

    }

    // Update is called once per frame
    void Update()
    {
        //eye.material.
    }

    private void FixedUpdate()
    {
        eyeRenderer.material.SetColor("_EmissionColor", Color.Lerp(Color.black, eyeEmissionColor, timer/interval));

        if (!retracted && playerTransform != null)
        {

            Vector3 goal = new Vector3(playerTransform.position.x, emissionPoint.position.y, playerTransform.position.z);
            if (trackingPlayer)
            {
                Vector3 movement = goal - previousPlayerPosition;
                float distance = Vector3.Distance(transform.position, playerTransform.position);
                previousPlayerPosition = goal;
                goal = goal + movement / Time.deltaTime * distance / shootForce / 2;

            }
            else
            {
                previousPlayerPosition = goal;
                trackingPlayer = true;
            }
            lookingAt = Vector3.Lerp(lookingAt, goal, Time.deltaTime * 4);
            if(rotationLimit == 0 || Mathf.Abs(Vector3.Angle(transform.forward, lookingAt - headPivot.position)) <= rotationLimit/2)
            {
                headPivot.LookAt(lookingAt);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    RaycastHit hit;
                    Vector3 targetVector = new Vector3(playerTransform.position.x, emissionPoint.position.y, playerTransform.position.z) - emissionPoint.position;
                    Debug.DrawRay(emissionPoint.position, (new Vector3(playerTransform.position.x, emissionPoint.position.y, playerTransform.position.z) - emissionPoint.position), Color.red);
                    if (Physics.Raycast(emissionPoint.position, targetVector.normalized, out hit, targetVector.magnitude, 0, QueryTriggerInteraction.Ignore))
                    {
                        Debug.Log(hit.collider.gameObject);
                        if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Shield"))
                        {
                            Fire();
                        }
                    }
                    else
                    {
                        Fire();
                    }

                    timer = 0;
                    audioSource.Stop();

                }
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

    public void Retract()
    {
        retracted = true;
        anim.enabled = true;
        anim.SetTrigger("Retract");
        StartCoroutine(ExtendAfter(8));
    }

    private IEnumerator ExtendAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("Extend");
        yield return new WaitForSeconds(0.5f);
        anim.enabled = false;
        retracted = false;
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
            audioSource.Stop();

            timer = 0;
            playerTransform = null;
            trackingPlayer = false;

        }
    }
}
