using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigg Ground " + other.name);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Ground" + collision.gameObject.name);
        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero;
        collision.rigidbody.isKinematic = true;

        Vector3 bounce =  collision.gameObject.transform.position - collision.GetContact(0).point;
        bounce.Normalize();
        bounce *= 10;
        collision.gameObject.transform.parent.GetComponent<gravity>().bounce = bounce;

    }

    private void OnCollisionExit(Collision collision)
    {
       
        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero;
        collision.rigidbody.isKinematic = false;

        

    }
}
