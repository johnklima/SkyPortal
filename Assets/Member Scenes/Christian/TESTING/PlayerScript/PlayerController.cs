using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3;
    public float runSpeed = 5;
    public float jumpheight = 0.8f;
    public float maxSlopeHeight = 5.16f;
    private float distToWall = 0.1f;
    private LayerMask groundLayer;
    private ObjectMass groundChecker;
    private Transform cameraT;
    private bool running;
    private bool isJumping;
    private float gravity = -9.81f;
    private float jumpVelocity;


    public bool isUsingJetpack;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");

        groundChecker = transform.gameObject.GetComponent<ObjectMass>();
        
    }

    private void Update()
    {
        MoveCharacter(new Vector3(Input.GetAxis("Horizontal"), jumpVelocity, Input.GetAxis("Vertical")));

        SlopeCheck();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        
        if (groundChecker.isGrounded)
        {
            isJumping = false;
        }
        
    }

    private void MoveCharacter(Vector3 direction)
    {
        if (groundChecker.isGrounded)
        {
            running = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            running = false;
        }
        
        switch (running)
        {
            case true:
                transform.Translate(direction * runSpeed * Time.deltaTime);
                break;
            
            case false:
                transform.Translate(direction * walkSpeed * Time.deltaTime);
                break;

        }

    }
    
    void Jump()
    {
        if (groundChecker.isGrounded && !isJumping)
        {
            jumpVelocity = Mathf.Sqrt(-3 * gravity * jumpheight);

            isJumping = true;

        }
    }


    private void SlopeCheck()
    {
        // Front
        
        if (groundChecker.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,  Quaternion.Euler(0,45,0)* transform.forward, out hit, 100f, groundLayer))
            {
                if (hit.distance < distToWall)
                {
                    Debug.Log("Slope Detected In Front");

                    switch (hit.transform.localScale.y < maxSlopeHeight)
                    {
                        case true:
                            Debug.Log("Moving Player Up");
                            transform.position = new Vector3(transform.position.x ,hit.collider.bounds.extents.y,transform.position.z);
                            break;
                    }
                }
            }
        }
        
        // Behind
        
        if (groundChecker.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,  Quaternion.Euler(0,45,0)* -transform.forward, out hit, 100f, groundLayer))
            {
                if (hit.distance < distToWall)
                {
                    Debug.Log("Slope Detected In Behind");

                    switch (hit.transform.localScale.y < maxSlopeHeight)
                    {
                        case true:
                            Debug.Log("Moving Player Up");
                            transform.position = new Vector3(transform.position.x ,hit.collider.bounds.extents.y,transform.position.z);
                            break;
                    }
                }
            }
        }
        
        
        // Right
        
        if (groundChecker.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,  Quaternion.Euler(0,0,45)* transform.right, out hit, 100f, groundLayer))
            {
                if (hit.distance < distToWall)
                {
                    Debug.Log("Slope Detected On The Right");

                    switch (hit.transform.localScale.y < maxSlopeHeight)
                    {
                        case true:
                            Debug.Log("Moving Player Up");
                            transform.position = new Vector3(transform.position.x ,hit.collider.bounds.extents.y,transform.position.z);
                            break;
                    }
                }
            }
        }
        
        // Left
        
        if (groundChecker.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,  Quaternion.Euler(0,0,45)* -transform.right, out hit, 100f, groundLayer))
            {
                if (hit.distance < distToWall)
                {
                    Debug.Log("Slope Detected On The Left");

                    switch (hit.transform.localScale.y < maxSlopeHeight)
                    {
                        case true:
                            Debug.Log("Moving Player Up");
                            transform.position = new Vector3(transform.position.x ,hit.collider.bounds.extents.y,transform.position.z);
                            break;
                    }
                }
            }
        }
    }
    
}
