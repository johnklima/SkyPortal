﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour {


    //gravity in meters per second per second
    public float GRAVITY_CONSTANT =  -9.8f;                      // -- for earth,  -1.6 for moon 

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector
    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame

    public Vector3 jump;
   
    public float mass = 1.0f;
    public float energy = 10000.0f;


    Vector3 prevPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        handleInput();

        handleMovement();
        
        
	}

    void handleInput()
    {
        //one shot jump
        if (Input.GetKeyDown(KeyCode.Space) && energy > 0.0f)
        {
            jump.y = 5.0f;
            energy -= 1.0f * Time.deltaTime;
        }
        /*
        if (Input.GetKey(KeyCode.A) && energy > 0.0f)
        {
            thrust.x = -5.0f;
            energy -= 0.25f * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.D) && energy > 0.0f)
        {
            thrust.x = 5.0f;
            energy -= 0.25f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && energy > 0.0f)
        {
            thrust.z = -5.0f;
            energy -= 0.25f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && energy > 0.0f)
        {
            thrust.z = 5.0f;
            energy -= 0.25f * Time.deltaTime;
        }
        */
    }
    
    void handleMovement()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
        finalForce += thrust;
        
        acceleration = finalForce / mass;
        velocity += acceleration * Time.deltaTime;
        
        //jump is a oneshot impulse
        velocity += jump;
        //move the player
        transform.position += velocity * Time.deltaTime;
        /*
                if (transform.position.x > 100 || transform.position.x < -100 )
                { 
                    transform.position -= new Vector3(velocity.x * Time.deltaTime, 0, 0);
                    velocity.x = 0;
                }


                if (transform.position.z > 100 || transform.position.z < -100)
                {
                    transform.position -= new Vector3(0, 0, velocity.z * Time.deltaTime);
                    velocity.z = 0;
                }
        */
        //check ground and undo if we are lower
        LayerMask mask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up ,-transform.up, out hit, 100f,mask))
        {
            if (hit.distance < 1.0f)
            {
                transform.position -= new Vector3(0, velocity.y * Time.deltaTime, 0);
                velocity.y = 0;
            }
        }
        //reset thrust
        thrust = Vector3.zero;
        jump = Vector3.zero;
    }

    
}
