using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Transform spikes;
    public float spikesDuration = 5f;
    public float cooldown = 5f;
    private bool activated = false;
    private float cooldownTimer = 0f;

    public BoxCollider damageCollider;

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
        if (!activated)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown){
                StartCoroutine(spikesRoutine());
                cooldownTimer = 0;
            }
        }
        
    }

    private IEnumerator spikesRoutine()
    {
        activated = true;
        float timer = 0f;
        Vector3 targetPosition = spikes.position + Vector3.up * 1.75f;
        Vector3 originalPosition = spikes.position;

        float riseDuration = spikesDuration / 4f;
        float fallDuration = 3 * riseDuration;


        damageCollider.enabled = true;
        while (timer<riseDuration)
        {
            timer += Time.fixedDeltaTime;
            float fraction = timer / riseDuration;
            float weight = Mathf.Sin(Mathf.PI/2 * fraction);;
            spikes.position = Vector3.Lerp(originalPosition, targetPosition, weight);
            yield return new WaitForFixedUpdate();
        }
        timer = 0;
        while (timer < fallDuration)
        {
            timer += Time.fixedDeltaTime;
            float fraction = timer / fallDuration;
            float weight = Mathf.Sin(Mathf.PI/2 * fraction);
            spikes.position = Vector3.Lerp(targetPosition, originalPosition, weight);
            yield return new WaitForFixedUpdate();
        }

        activated = false;
        damageCollider.enabled = false;
    }

}
