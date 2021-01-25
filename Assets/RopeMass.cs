using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMass : MonoBehaviour
{

    public float mass = 1;
    public Vector3 velocity;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        //init everything to zero
        transform.position = Vector3.zero;
        velocity = Vector3.zero;
        force = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        force = Vector3.zero;
    }
    public void applyForce(Vector3 a_force)
    {
        force += a_force;

    }
    /*
  void simulate(float dt) method calculates the new velocity and new position of 
  the Mass according to change in time (dt). Here, a simulation method called
  "The Euler Method" is used. The Euler Method is not always accurate, but it is 
  simple. It is suitable for most of physical simulations that we know in common 
  computer and video games.
*/
    public void simulate(float dt)
    {
        velocity += (force / mass) * dt;        // Change in velocity is added to the velocity.
                                                // The change is proportinal with the acceleration (force / m) and change in time

        //transform.position += velocity * dt;   // Change in position is added to the position.
                                               // Change in position is velocity times the change in time
    }
}
