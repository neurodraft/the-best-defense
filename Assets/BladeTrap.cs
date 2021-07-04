using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    /*public Transform blade;
    public float bladeDuration = 5f;
    public float cooldown = 5f;
    private bool activated = false;
    private float cooldownTimer = 0f;*/

    public float speed = 1f;
    public float maxRotation = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        /*if (!activated)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                StartCoroutine(bladeRoutine());
                cooldownTimer = 0;
            }
        }*/
    }

    /*private IEnumerator bladeRoutine()
    {
        activated = true;
        float timer = 0f;
        Vector3 targetPosition = blade.position + Vector3.up * 1.75f;
        Vector3 originalPosition = blade.position;

        while (timer < bladeDuration)
        {
            timer += Time.fixedDeltaTime;
            float fraction = timer / bladeDuration;
            float weight = Mathf.Sin(Mathf.PI * fraction);
            blade.position = Vector3.Lerp(originalPosition, targetPosition, weight);
            yield return new WaitForFixedUpdate();
        }

        activated = false;
    }*/
}
