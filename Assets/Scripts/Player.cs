﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 4.0f;
    private float rotationSpeed = 4.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public CameraHelper cameraHelper;

    public GameObject shield;

    private Animator animator;

    private bool isMoving = false;
    private bool isShieldActive;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        handleShield();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized    ;

        if (isMoving && move == Vector3.zero)
        {
            isMoving = false;
            animator.SetBool("isRunning", isMoving);
        }
        else if(!isMoving && move != Vector3.zero)
        {
            isMoving = true;
            animator.SetBool("isRunning", isMoving);
        }


        if (cameraHelper != null)
        {
            Quaternion cameraRotation = cameraHelper.transform.rotation;
            move = cameraRotation * move;
        }


        if (isShieldActive)
        {
            rotatePlayerTowardsCursor();
            move = move * playerSpeed*0.5f;
            float angle = Mathf.Abs(Vector3.Angle(move, transform.forward));
            animator.SetBool("isWalkingBackward", angle > 90);

        } else
        {
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
        Debug.Log(actualPlayerSpeed);

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            setShieldActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            setShieldActive(false);

        }
    }

    private void setShieldActive(bool val)
    {
        isShieldActive = val;
        shield.SetActive(val);
        animator.SetBool("isShieldActive", val);
    }

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
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

}
