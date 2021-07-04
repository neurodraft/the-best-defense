using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //private Animator animator;

    private bool boosting = false;

    private bool slamming = false;

    private bool charging = false;

    private float charge = 0;

    private Vector3 chargedPosition;
    private Vector3 normalPosition;

    private Vector3 slamPosition;

    public ParticleSystem impactParticleSystem;

    public Transform cameraTracker;
   

    void Start()
    {
        normalPosition = transform.localPosition;
        chargedPosition = normalPosition - new Vector3(0, 0, 0.1f);
        slamPosition = normalPosition + new Vector3(0, 0, 0.1f);

        //this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!charging && !slamming && Input.GetMouseButtonDown(0))
        {
            charging = true;
            StartCoroutine(ChargeSlam());
        } else if(charging && !slamming && Input.GetMouseButtonUp(0))
        {
            charging = false;
            slamming = true;
            StopCoroutine(ChargeSlam());
            StartCoroutine(SlamShield());
        }

        /*
        if (!boosting && Input.GetMouseButtonDown(1))
        {
            //animator.SetTrigger("slam");
            StartCoroutine(EnableBoostingFor(0.2f));
        }
        */
    }

    public void OnDisable()
    {
        StopAllCoroutines();
        charging = false;
        boosting = false;
        slamming = false;
        transform.localPosition = normalPosition;
    }

    private IEnumerator ChargeSlam()
    {
        charge = 0;
        yield return new WaitForFixedUpdate();

        while (charging && charge < 1f)
        {
            Debug.Log("Charging");

            charge = Mathf.Clamp(charge + Time.fixedDeltaTime, 0f, 1f);
            //Debug.Log("Charge:" + charge);

            cameraTracker.localPosition = new Vector3(cameraTracker.localPosition.x, charge * 4f, cameraTracker.localPosition.z);
            transform.localPosition = Vector3.Lerp(normalPosition, chargedPosition, charge);
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator SlamShield()
    {
        cameraTracker.localPosition = new Vector3(cameraTracker.localPosition.x, 0, cameraTracker.localPosition.z);

        Debug.Log("Slamming");
        Vector3 initalPosition = transform.localPosition;
        float slamTimer = 0;
        boosting = true;
        while (slamTimer < 2f)
        {
            yield return new WaitForFixedUpdate();
            slamTimer += Mathf.Clamp(slamTimer + (Time.fixedDeltaTime), 0f, 1f); ;
            transform.localPosition = Vector3.Lerp(initalPosition, slamPosition, slamTimer);
        }
        boosting = false;
        slamTimer = 0;
        while (slamTimer < 0.5f)
        {
            yield return new WaitForFixedUpdate();
            slamTimer += Mathf.Clamp(slamTimer + (Time.fixedDeltaTime), 0f, 1f); ;
            transform.localPosition = Vector3.Lerp(slamPosition, normalPosition, slamTimer);
        }
        transform.localPosition = normalPosition;

        slamming = false;
    }


    IEnumerator EnableBoostingFor(float duration)
    {
        boosting = true;
        yield return new WaitForSeconds(duration);
        boosting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile;
            if(collision.gameObject.TryGetComponent<Projectile>(out projectile)){
                if (boosting)
                {
                    projectile.Boost(charge);

                    if(impactParticleSystem != null)
                    {
                        
                        impactParticleSystem.Play();
                    }
                } else
                {
                    projectile.disappear();
                }
            }
        }
    }
    

}
