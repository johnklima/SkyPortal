using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Core physics for rope
public class RopeSystem : MonoBehaviour
{

    public RopeMass[] masses; //each link in the chain
    public Spring[] springs ;  //pair of 2 masses forming the spring

    public int masscount;
    public float massEach;
    public float springConstant;                               //A constant to represent the stiffness of the spring
    public float springLength = 1.0f;                          //The length that the spring does not exert any force
    public float springFrictionConstant;
    public Vector3 gravitation;                        //gravitational acceleration (gravity will be applied to all Masses)
    public Vector3 ropeConnectionPos;                  //a point in space that is used to set the position of the 
                                                       //first Mass in the system (Mass with index 0)

    public Vector3 ropeConnectionVel;                   //a variable to move the ropeConnectionPos 
    public Vector3 ropeConnectionEndPos;                //a point in space that is used to set the position of the 
                                                        //last Mass in the system (Mass with index num masses)

    public float groundRepulsionConstant;                      //a constant to represent how much the ground shall repel the Masses
    public float groundFrictionConstant;                       //a constant of friction applied to Masses by the ground
                                                        //(used for the sliding of rope on the ground)
    float groundAbsorptionConstant;                     //a constant of absorption friction applied to Masses by the ground
                                                        //(used for vertical collisions of the rope with the ground)
    public float groundHeight;                                 //a value to represent the z position value of the ground                                                               //(the ground is a planer surface facing +z direction)
    public float groundOffset;
    public bool usesEndPos;                                    //is the last spring ALSO movable/anchorable
    public float airFrictionConstant;						 //a constant of air friction applied to Masses


    private void Start()
    {
        //hardcode some values
        /*
        massEach = 1.0f;                    // Each Particle Has A Weight Of 50 Grams
        springConstant = 1.0f;              // springConstant In The Rope
        springLength = 0.5f;                // Normal Length Of Springs In The Rope
        springFrictionConstant = 0.2f;      // Spring Inner Friction Constant
        gravitation = new Vector3(0, -9.81f, 0);           // Gravitational Acceleration
        airFrictionConstant = 0.02f;        // Air Friction Constant
        groundRepulsionConstant = 100.0f;   // Ground Repel Constant
        groundFrictionConstant = 0.2f;      // Ground Slide Friction Constant
        groundAbsorptionConstant = 2.0f;    // Ground Absoption Constant
        groundHeight = -1.0f;               // Height Of Ground

        */

        //lets buffer our masses in an array
        masscount = transform.childCount;
        masses = new RopeMass [masscount];
        int i = 0;
        foreach (Transform child in transform)
        {
            child.position += new Vector3(i * springLength, 0, 0);
            masses[i] = child.GetComponent<RopeMass>();            
            masses[i].mass = massEach;
            
            i++;
          
        }

        //and create the springs, these are attached to the rope mass gameobject
        //so they easy create at runtime, though they do nothing to object directly
        //the last "spring" is abandoned. must be some c# way to instance an simple class??
        springs = new Spring[masscount - 1];
       

        for (int c = 0; c < masscount - 1; c++)           //to create each spring, start a loop
        {
            //Create the spring with index "c" by the Mass with index "c" and another Mass with index "c + 1".
            springs[c] = masses[c].transform.GetComponent<Spring>();
            
            springs[c].mass1 = masses[c];
            springs[c].mass2 = masses[c + 1];

            springs[c].springConstant = springConstant;
            springs[c].springLength = springLength;
            springs[c].springFrictionConstant = springFrictionConstant;
           
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {

        
        init();                        // Step 1: reset forces to zero
        solve();                       // Step 2: apply forces
        simulate(Time.deltaTime);      // Step 3: iterate the Masses by the change in time
        


    }
    void init()
    {
        for (int i = 0; i < masscount; i++)       // We will iterate every Mass
            masses[i].init();
    }
    void solve()
    {
        for (int i = 0; i < masscount - 1; i++)         //apply force of all springs
        {
            if (i == masscount - 2 && usesEndPos)       //numOfMasses - 2 is the spring connecting the last 2 masses
            {
                springs[i].solve(true); 
            }                   
            else
            { 
                springs[i].solve(false); 
            }
                
        }

        
        for (int a = 0; a < masscount; ++a)               //Start a loop to apply forces which are common for all Masses
        {

            masses[a].applyForce(gravitation * masses[a].mass);                    //The gravitational force
            //masses[a].applyForce(-masses[a].velocity * airFrictionConstant);      //The air friction


            if (false && masses[a].transform.position.y  < groundHeight)        //Forces from the ground are applied if a Mass collides with the ground
            {
                Vector3 v = Vector3.zero;            //A temporary vector

                v = masses[a].velocity;                     //get the velocity
                v.y = 0;                                    //omit the velocity component in z direction

                //The velocity in y direction is omited because we will apply a friction force to create 
                //a sliding effect. Sliding is parallel to the ground. Velocity in z direction will be used
                //in the absorption effect.
                masses[a].applyForce(-v * groundFrictionConstant);     //ground friction force is applied

                v = masses[a].velocity;             //get the velocity
                v.x = 0;                            //omit the x and z components of the velocity
                v.z = 0;                            //we will use v in the absorption effect


                //above, we obtained a velocity which is vertical to the ground and it will be used in 
                //the absorption force

                if (v.y < 0)                                                       //let's absorb energy only when a Mass collides towards the ground
                   masses[a].applyForce(-v * groundAbsorptionConstant);       //the absorption force is applied

                //The ground shall repel a Mass like a spring. 
                //By "NiPoint3(0, 0, 0)" we create a vector in the plane normal direction 
                //with a magnitude of groundRepulsionConstant.
                //By (groundHeight - Masses[a]->pos.z) we repel a Mass as much as it crashes into the ground.
                Vector3 force = new Vector3(0, groundRepulsionConstant,0) * (groundHeight - masses[a].transform.position.y);
                
                masses[a].applyForce(force);           //The ground repulsion force is applied
            }

        }


    }
    void simulate(float dt)
    {
        for (int i = 0; i < masscount; i++)       // We will iterate every Mass
            masses[i].simulate(dt);

        ropeConnectionPos += ropeConnectionVel * dt;    //iterate the positon of ropeConnectionPos


        if (false && ropeConnectionPos.y < groundHeight)         //ropeConnectionPos shall not go under the ground
        {
            ropeConnectionPos.y = groundHeight;
            ropeConnectionVel.y = 0;
        }



        masses[0].transform.position  = ropeConnectionPos;             //Mass with index "0" shall position at ropeConnectionPos
        masses[0].velocity  = ropeConnectionVel;                        //the Mass's velocity is set to be equal to ropeConnectionVel

        
        if (usesEndPos)
        {
            masses[masscount - 1].transform.position = ropeConnectionEndPos;    //Mass with index last shall position at ropeConnectionEndPos      
        }
        
    }

}

