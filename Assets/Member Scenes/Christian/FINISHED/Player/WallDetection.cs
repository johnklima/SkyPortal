using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    private Transform player;
    public float pushForce;
    public float distance;

    private SlopeDetection _slopeDetection;

    private void Start()
    {
        // Cashes the player Object
        player = GameObject.FindWithTag("Player").transform.parent;
        // Cashes our SlopeDetection script
        _slopeDetection = player.gameObject.GetComponent<SlopeDetection>();
    }

    // As long as collision collision is happening.
    private void OnCollisionStay(Collision other)
    {
        // Check first if its the player since we dont want to interact with anything else
        if (other.gameObject.tag == "Player"){
            
            // This is only to check if we can walk over the current object or not from the SlopeDetection or rather StepDectection
            if (_slopeDetection.cantWalkOver)
            {
                // Collision Feedback for debugging
                Debug.Log("collided");

                // Stores the Contact point, i never ended up using this though 
                //Vector3 pushBack = other.GetContact(0).point - player.gameObject.transform.position;

                // Gets the distance between the wall and the player
                float dist = Vector3.Distance(transform.position, player.transform.position);

                // Checks if we are in distance this might be a useless check? haven't tried moving the rest of the code out of it and im really tired now...
                if (dist < distance)
                {
                    // Storing the direction of entry / Collision
                    Vector3 dir = transform.position - player.transform.position;

                    // We dont want to move upwards so we just put this out of the question by doing this
                    dir.y = 0;
                
                    // Push the player outwards from the point of entry
                    // BUG (There might be a bug caused by this which is that if you're inside the object it will push you further into it since it thinks you're coming from that direction)
                    player.transform.Translate(dir.normalized * pushForce * Time.deltaTime);
                
                    // If we got collision then send feedback message to console 
                    Debug.Log("Push!!!!");
                }
            }
        }
    }
}
