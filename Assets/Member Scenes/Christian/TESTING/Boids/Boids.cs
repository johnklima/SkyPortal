using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boids : MonoBehaviour
{
    public Transform flock;

    public float cohesionFactor = 0.2f;
    public float seperationFactor = 3f;
    public float alignmentFactor = 1f;
    public float constraintFactor = 0.95f;

    public float speed = 2.93f;

    public float collisionDistance = 0.95f;

    public Vector3 velocity;

    public Transform[] points;
    public int destinationPoint;
    public bool pathPending;
    public float distToSwitchPoints;
    private Vector3 destination;

    public float returnCooldown = 0f;
    public bool playerClose = false;


    // Start is called before the first frame update
    void Start()
    {
        flock = transform.parent;
        
        Vector3 pos = new Vector3(Random.Range(0f,3f),Random.Range(0f,3f),Random.Range(0f,3f));
        Vector3 look = new Vector3(Random.Range(0f,3f),Random.Range(0f,3f),Random.Range(0f,3f));

        float speed = Random.Range(0f, 10f);

        transform.position = pos;
        transform.LookAt(look);

        velocity = (look - pos) * speed;
        
        GoToNextPoint();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        PathPendingCheck();

        Vector3 newVelocity = new Vector3(0,0,0);
        newVelocity += Cohesion() * 0.5f;

        newVelocity += Seperation() * seperationFactor;

        newVelocity += Alignment() * alignmentFactor;

        newVelocity += Constraint() * constraintFactor;

        Vector3 slerpVec = Vector3.Slerp(velocity, newVelocity, Time.deltaTime);

        velocity = slerpVec;

        transform.position += velocity * (Time.deltaTime * speed);
        transform.LookAt(transform.position + velocity);

        if (returnCooldown > 0)
        {
            collisionDistance = 8;
            returnCooldown -= Time.deltaTime;
        }
        else
        {
            collisionDistance = 0.95f;
        }

        
    }
    
    // separation: steer to avoid crowding local flockmates
    // alignment: steer towards the average heading of local flockmates
    // cohesion: steer to move towards the average position (center of mass) of local flockmates

    Vector3 Cohesion()
    {
        Vector3 steer = new Vector3(0,0,0);

        int sibs = 0; // Count Boids, Might vary

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += (Vector3)boid.transform.position;
                sibs++;
            }
        }

        steer /= sibs;
        steer -= (Vector3)transform.position;

        steer.Normalize();

        return steer;

    }

    Vector3 Seperation()
    {
        Vector3 steer = new Vector3(0,0,0);
        int sibs = 0;

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                if ((transform.position - boid.transform.position).magnitude < collisionDistance)
                {
                    steer += (Vector3) (transform.position - boid.transform.position);
                    sibs++;
                }
            }
        }

        steer /= sibs;
        steer.Normalize();
        return steer;

    }

    Vector3 Alignment()
    {
        Vector3 steer = new Vector3(0,0,0);
        int sibs = 0;

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += boid.GetComponent<Boids>().velocity;
                sibs++;
            }
        }

        steer /= sibs;
        
        steer.Normalize();

        return steer;
    }

    Vector3 Constraint()
    {
        Vector3 steer = new Vector3();

        steer += (destination - transform.position);
        
        steer.Normalize();
        return steer;
    }

    void GoToNextPoint()
    {
        if(points.Length == 0) return;

        pathPending = true;

        destination = points[destinationPoint].position;

        destinationPoint = (destinationPoint + 1) % points.Length;

    }

    void PathPendingCheck()
    {
        if (!pathPending && transform.position.magnitude < distToSwitchPoints)
        {
            GoToNextPoint();
        }

        if (pathPending && transform.position.magnitude > distToSwitchPoints)
        {
            pathPending = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player") && !playerClose)
        {
            returnCooldown = 1f;
            playerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && playerClose)
        {
            playerClose = false;
        }
    }
}
