using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Transform flock;
    public float cohesionFactor = 0.5f;
    public float separationFactor = 3.0f;
    public float allignFactor = 2.0f;
    public float constrainFactor = 2.0f;

    public float collisionDistance = 6.0f;
    public float speed = 20.0f;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        flock = transform.parent;

        Vector3 pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        Vector3 look = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        float speed = Random.Range(0f, 10f);


        transform.position = pos;
        transform.LookAt(look);
        velocity = (look - pos) * speed;



    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = new Vector3(0, 0, 0);
        // rule 1 all boids steer towards center of mass - cohesion
        newVelocity += cohesion() * cohesionFactor;


        // rule 2 all boids steer away from each other - avoidance        
        newVelocity += separation() * separationFactor;

        // rule 3 all boids match velocity - allignment
        newVelocity += align() * allignFactor;

        newVelocity += constrain() * constrainFactor;

        Vector3 slerpVelo = Vector3.Slerp(velocity, newVelocity, Time.deltaTime);
        
        velocity = slerpVelo;

        transform.position += velocity * Time.deltaTime * speed;
        transform.LookAt(transform.position + velocity );
    }

    Vector3 constrain()
    {
        Vector3 steer = new Vector3(0, 0, 0);

        steer += (Vector3.zero - transform.position);

        steer.Normalize();
        return steer;
    }


    Vector3 align()
   {
        Vector3 steer = new Vector3(0, 0, 0);
        int sibs = 0;

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += boid.GetComponent<Boid>().velocity;
                sibs++;
            }

        }
        steer /= sibs;

        steer.Normalize();

        return steer;
    }

    Vector3 separation()
    {
        Vector3 steer = new Vector3(0, 0, 0);
        int sibs = 0;


        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                if ((transform.position - boid.transform.position).magnitude < collisionDistance)
                {
                    steer += (Vector3)(transform.position - boid.transform.position);
                    sibs++;
                }

            }

        }
        steer /= sibs;
        steer.Normalize();        //unit, just direction
        return steer;

    }


    Vector3 cohesion()
    {
        Vector3 steer = new Vector3(0, 0, 0);

        int sibs = 0;           //count the boids, it might change

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += (Vector3)boid.transform.position;
                sibs++;
            }

        }
        
        steer /= sibs; //center of mass is the average position of all        
        
        steer -= (Vector3)transform.position;

        steer.Normalize();


        return steer;
    }
}
