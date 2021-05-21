using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator animator;

    private bool boosting = false;

    public ParticleSystem impactParticleSystem;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boosting && Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("slam");
            StartCoroutine(EnableBoostingFor(0.2f));
        }
    }

    public void OnDisable()
    {
        StopAllCoroutines();
        boosting = false;
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
                    projectile.Boost();

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
