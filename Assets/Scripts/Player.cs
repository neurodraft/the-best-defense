using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 4.0f;
    private float rotationSpeed = 8.0f;
    private float gravityValue = -9.81f;
    private bool hasKey = false;

    public CameraHelper cameraHelper;

    public GameObject shield;


    private Animator animator;

    public ParticleSystem damageParticleSystem;
    public Transform cameraTracker;


    private bool isMoving = false;
    private bool isShieldActive;

    public float maxHealth=100.0f;
    public float currentHealth = 0f;

    public float maxStamina = 100.0f;
    public float currentStamina = 0f;


    private bool canControl = true;

    public AudioClip hurtSound;
    private AudioSource audioSource;
    

  
    private void Start()
    {
      
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        EventManager.StartListening("gameOver", ResetState);
        EventManager.StartListening("player_damage", HandleDamage);

        addHealth(maxHealth);
        addStamina(maxStamina);
        EventManager.StartListening("key_picked_up", KeyPickedUp);

    }

    private void KeyPickedUp(Dictionary<String,object> message)
    {
        hasKey = true;
        Debug.Log("Player has key: " + hasKey);
        //Show key in UI
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void ResetState(Dictionary<string, object> message)
    {

    }

    public void AddHealth(Dictionary<string, object> message)
    {

    }
    public void HandleDamage(Dictionary<string, object> message)
    {
        setShieldActive(false);
        StartCoroutine(DisableControl(1f));

        audioSource.PlayOneShot(hurtSound);

        float amount = (float)message["amount"];
        addHealth(-amount);


        Vector3 direction = (Vector3)message["direction"];
        Vector3 position = (Vector3)message["position"];
       
        controller.Move(direction);
        if(damageParticleSystem != null)
        {
            damageParticleSystem.transform.position = position;
            damageParticleSystem.Play();
        }

    }

    IEnumerator DisableControl(float duration)
    {
        canControl = false;
        Debug.Log("canControl = false");

        yield return new WaitForSeconds(duration);

        canControl = true;
        Debug.Log("canControl = true");

    }

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        Vector3 move = Vector3.zero;

        if (canControl)
        {
            handleShield();

            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (cameraHelper != null)
            {
                Quaternion cameraRotation = cameraHelper.transform.rotation;
                move = cameraRotation * move;
            }

        }

        if (isMoving && move == Vector3.zero)
        {
            isMoving = false;
            animator.SetBool("isRunning", isMoving);
        }
        else if (!isMoving && move != Vector3.zero)
        {
            isMoving = true;
            animator.SetBool("isRunning", isMoving);
        }

        if (isShieldActive)
        {
            rotatePlayerTowardsCursor();
            move = move * playerSpeed * 0.5f;
            float angle = Mathf.Abs(Vector3.Angle(move, transform.forward));
            animator.SetBool("isWalkingBackward", angle > 90);

        }
        else
        {
            if(currentStamina < maxStamina)
            {
                float reg = 5f * Time.deltaTime;
                Debug.Log("Regenerating " + reg +  " stamina.");
                addStamina(reg);
            }
            
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            move = move * playerSpeed;
        }


        /*
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move((move + playerVelocity) * Time.deltaTime);

        float actualPlayerSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        //Debug.Log(actualPlayerSpeed);

        animator.SetFloat("playerSpeed", actualPlayerSpeed/playerSpeed);
    }

    private void rotatePlayerTowardsCursor()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void handleShield()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            setShieldActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            setShieldActive(false);

        }
    }

    public void setShieldActive(bool val)
    {
        isShieldActive = val;
        shield.SetActive(val);
        animator.SetBool("isShieldActive", val);

        if (val)
        {
            cameraTracker.localPosition = Vector3.forward * 4;
        }
        else
        {
            cameraTracker.localPosition = Vector3.zero;
        }
    }

    /*
    // this script pushes all rigidbodies that the character touches
    float pushPower = 2.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (!isShieldActive && (collision.gameObject.CompareTag("Projectile") ))
        {
            currentHealth -= 1;
        }
    }

   
    public float getCurrentHealth()
    {
        return currentHealth;
    }
    public float getCurrentStamina()
    {
        return currentStamina;
    }
    public bool getIsShieldActive()
    {
        return isShieldActive;
    }
    public void addStamina(float value)
    {
        currentStamina = Mathf.Clamp(currentStamina + value, 0, maxStamina);
        EventManager.TriggerEvent("player_stamina_update", new Dictionary<string, object> { { "current", currentStamina }, { "max", maxStamina } });

    }
    public void addHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        EventManager.TriggerEvent("player_health_update", new Dictionary<string, object> { { "current", currentHealth }, { "max", maxHealth } });
    }
    
}
    
    
   
   


