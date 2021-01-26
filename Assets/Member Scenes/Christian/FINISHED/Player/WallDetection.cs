using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    public GameObject player;
    public float pushForce;
    public float distance;

    private SlopeDetection _slopeDetection;

    private void Start()
    {
        _slopeDetection = player.gameObject.GetComponent<SlopeDetection>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player"){

            if (_slopeDetection.cantWalkOver)
            {
                Debug.Log("collided");

                Vector3 pushBack = other.GetContact(0).point - player.gameObject.transform.position;

                float dist = Vector3.Distance(transform.position, player.transform.position);

                if (dist < distance)
                {
                    Vector3 dir = transform.position - player.transform.position;

                    dir.y = 0;
                
                    player.transform.Translate(dir.normalized * pushForce * Time.deltaTime);
                
                    Debug.Log("poosh");
                }
            }
        }
    }
}
