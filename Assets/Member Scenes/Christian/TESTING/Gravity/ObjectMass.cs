using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMass : MonoBehaviour
{
    public float mass;
    public Vector3 velocity = new Vector3(0,0,0);
    public Vector3 acceleration = new Vector3(0,0,0);
    public Vector3 finalForce = new Vector3(0,0,0);
    public bool isGrounded;
    public float distToGround = 0f;
    private float slopeAngle;
    private float vertical;
    public LayerMask groundLayer;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down,distToGround + 0.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
