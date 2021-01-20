using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private GameObject parentObject;

    private ObjectMass _objectMass;
    
    private void Start()
    {
        parentObject = transform.parent.gameObject;
        
        if (parentObject)
        {
            _objectMass = parentObject.GetComponent<ObjectMass>();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Grounded");
            _objectMass.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Not Grounded");
            _objectMass.isGrounded = false;
        }
    }
}
