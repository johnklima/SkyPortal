using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public ObjectMass[] gravityObjects;

    public float gravity = -9.8f;
    
    private void Start()
    {
        gravityObjects = FindObjectsOfType<ObjectMass>();
    }

    private void FixedUpdate()
    {
        GravityPull();
    }

    private void GravityPull()
    {
        foreach (ObjectMass Omass in gravityObjects)
        {
            
            if (!Omass.isGrounded)
            {
                gravity = -9.8f;
                Omass.finalForce.Set(0, gravity * Omass.mass, 0);
                Omass.acceleration = Omass.finalForce / Omass.mass;
                Omass.velocity += Omass.acceleration * Time.deltaTime;

                Omass.gameObject.transform.position += Omass.velocity * Time.deltaTime;
                
            }
            /*else
            {
                gravity = -0.1f;
                Omass.finalForce.Set(0, gravity * Omass.mass, 0);
                Omass.acceleration = Omass.finalForce / Omass.mass;
                Omass.velocity += Omass.acceleration * Time.deltaTime;

                Omass.gameObject.transform.position += Omass.velocity * Time.deltaTime;
            }*/
            
        }
    }

}
