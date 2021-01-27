using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    public float maxSlopeHeight;
    public Vector3 heightOffset;
    private float minDist = 0.1f;

    private GameObject player;
    public LayerMask _mask = 1 << 15;
    public bool cantWalkOver;
    
    private void Start()
    {
        player = this.gameObject;
    }


    private void FixedUpdate()
    {
        
        // Checks for Steps on the Front
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position + heightOffset, transform.forward, out hit, 10f, _mask))
        {
            if (hit.collider.bounds.extents.y - transform.position.y < maxSlopeHeight)
            {

                if (hit.distance <= minDist)
                {
                    transform.Translate(Vector3.up * hit.collider.bounds.extents.y);
                    Debug.Log("Moved the player up :)");
                }

            }

            else
            {
                if (hit.distance < minDist + 0.4f)
                {
                    cantWalkOver = true;
                }
                else
                {
                    cantWalkOver = false;
                }

            }
            
            Debug.Log("Found Slope in Front");

        }
        
        // Checks for Steps on the Behind
        
        
        if (Physics.Raycast(transform.position + heightOffset, -transform.forward, out hit, 10f, _mask))
        {
            if (hit.collider.bounds.extents.y - transform.position.y < maxSlopeHeight)
            {

                if (hit.distance <= minDist)
                {
                    transform.Translate(Vector3.up * hit.collider.bounds.extents.y);
                    Debug.Log("Moved the player up :)");
                }

            }

            else
            {
                if (hit.distance < minDist + 0.4f)
                {
                    cantWalkOver = true;
                }
                else
                {
                    cantWalkOver = false;
                }

            }
            
            Debug.Log("Found Slope Behind");
        }
        
        // Checks for Steps on the right side
        
        if (Physics.Raycast(transform.position + heightOffset, transform.right, out hit, 10f, _mask))
        {
            if (hit.collider.bounds.extents.y - transform.position.y < maxSlopeHeight)
            {

                if (hit.distance <= minDist)
                {
                    transform.Translate(Vector3.up * hit.collider.bounds.extents.y);
                    Debug.Log("Moved the player up :)");
                }

            }

            else
            {
                if (hit.distance < minDist + 0.4f)
                {
                    cantWalkOver = true;
                }
                else
                {
                    cantWalkOver = false;
                }

            }
            
            Debug.Log("Found Slope On The Right");
        }
        
        // Checks for Steps on the left side
        
        if (Physics.Raycast(transform.position + heightOffset, -transform.right, out hit, 10f, _mask))
        {
            if (hit.collider.bounds.extents.y - transform.position.y < maxSlopeHeight)
            {

                if (hit.distance <= minDist)
                {
                    transform.Translate(Vector3.up * hit.collider.bounds.extents.y);
                    Debug.Log("Moved the player up :)");
                }

            }

            else
            {
                if (hit.distance < minDist + 0.4f)
                {
                    cantWalkOver = true;
                }
                else
                {
                    cantWalkOver = false;
                }

            }
        }
    }
}
