using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the spring class defines the behaviour between two rope masses
public class Spring : MonoBehaviour
{
    public float springConstant;                               //A constant to represent the stiffness of the spring
    public float springLength;                                 //The length that the spring does not exert any force
    public float springFrictionConstant;                       //A constant to be used for the inner friction of the spring

    public RopeMass mass1;                                        //The first Mass at one tip of the spring
    public RopeMass mass2;                                        //The second Mass at the other tip of the spring

    public void solve(bool endpoint = false)             //solve() method: the method where forces can be applied
    {
        Vector3 springVector = mass1.transform.position - mass2.transform.position;                            //vector between the two Masses
        float r = Vector3.Magnitude(springVector);                             //distance between the two Masses
        Vector3 force = Vector3.zero;                                                  //force initially has a zero value

        if (r != 0)                                                                                //to avoid a division by zero check if r is zero
            force += (springVector / r) * (r - springLength) * (-springConstant);   //the spring force is added to the force

        force += -(mass1.velocity - mass2.velocity) * springFrictionConstant;                     //the friction force is added to the force

        
        //with this addition we obtain the net force of the spring

        mass1.applyForce(force);

        if (!endpoint)
        {
            mass2.applyForce(-force);                                                  //the opposite of force is applied to Mass2      
        }

    }
}

