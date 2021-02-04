using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{

    public Vector3 targetPoint; 
    public IKSystem iksystem;
    public bool caught = false;
    public bool isThrown = false;
    SineWave wave;

    public PlayerScript player;

    float GRAVITY = 9.8f; //hmmm as a positive force

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform.position;
        wave = transform.GetComponent<SineWave>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isThrown)
            return;

        //slerp to the target point to create some motion
        transform.position = Vector3.Slerp(transform.position, targetPoint, Time.deltaTime * 30);

        float scale = Vector3.Distance(transform.position, transform.parent.position);
        scale = scale / iksystem.childcount;  

        iksystem.length = scale; //of each link

        if (Vector3.Distance(transform.position, targetPoint) < 1 )
        { 
            
            //iksystem.dragmode = true;
            //iksystem.reachmode = false;
            caught = true;
            wave.enabled = false;
            
            
        }
        else if (Vector3.Distance(transform.position, targetPoint) > 5)
        {
            //iksystem.dragmode = false;
            //iksystem.reachmode = true;
            caught = false;
            wave.enabled = true;
        }
            
    }

    public void reset()
    {
        transform.position = Vector3.zero;
        isThrown = false;
        caught = false;
    }
    public Vector3 fire(Vector3 startPos, Vector3 targPos)
    {
        targPos.y += 10;

        float f = calculateIterativeTrajectory(startPos, targPos,0);

       

        return (targPos - startPos).normalized * f;
    }

    float calculateIterativeTrajectory(Vector3 startPoint, Vector3 endPoint, float desiredAngle)
    {
        
        Vector3 t;
        Vector3 c;

        t = endPoint;
        c = startPoint;				

        //get a flat distance
        t.y = c.y = 0;            
        float flatdistance = Vector3.Distance(t, c);

        Vector3 P1, P2, p2;
        p2 = P2 = endPoint - startPoint;
        
        //get just the flat direction without y component
        p2.y = 0;
        p2.Normalize();
        P1 = p2;
      
		//unitize the vector
        P2.Normalize();

        //get angle in rads, this is a minimum launch angle (if down to up)
        float angle = Mathf.Acos(Vector3.Dot(P1,P2));

        //any angle less than 45 is 45 (optimal angle)
        //any angle greater is the angle, plus HALF the angle of the vector to y up.
        //with balistics the angle ALWAYS has to be greater than the direct angle from
        //point to point. so we add half of this angle to 90, splitting the difference

        if (angle< 0.785398163f)
            angle = 0.785398163f;
        else
            angle+= Mathf.Acos(Vector3.Dot(P1, Vector3.up)) * 0.5f ;
                     

        //if it is too close to pure vertical make it less than pure vertical
        if(angle > Mathf.PI/2 - 0.05f)
            angle = Mathf.PI / 2 - 0.05f;

        //if we are going from up to down, just use appx 60 degs
        if(startPoint.y > endPoint.y)
            angle = 0.985398163f;
        
        //if we are asked to use a specific angle, this is our angle..
        if(desiredAngle != 0 )
        {
            angle = Mathf.Deg2Rad * desiredAngle;
        }


        float Y = endPoint.y - startPoint.y;

        // perform the trajectory calculation to arrive at the gun powder charge aka 
        // target velocity for the distance we desire, based on launch angle
        float rng = 0;
        float Vo = 0;
        float trydistance = flatdistance;
        int iters = 0;

        //now iterate until we find the correct proposed distance to land on spot.
        //this method is based on the idea of calculating the time to peak, and then
        //peak to landing at the height differential. we can then extract an XY distance 
        //achieved regardless of height differential. by iterating with a binary heuristic
        //we increase or decrease the initial velocity until we acheive our XY distance

        float f = (Mathf.Sin(angle * 2.0f)); 
        if(f <= 0)
            f = 1.0f;      //just for safety (though this should never be)

        while (Mathf.Abs(rng - flatdistance) > 0.001f && iters < 64)
        {
            //make sure we dont squirt on a negative number. we can IGNORE that result
            if (trydistance > 0)
            {

                //create an initial force 
                float sqrtcheck = (trydistance * GRAVITY) / f;
                if (sqrtcheck < 0)
                    f = f; //meaningless ;)
                else
                    Vo = Mathf.Sqrt(sqrtcheck);

                //find the vector of it in our trajectory planar space
                float Vy = Vo * Mathf.Sin(angle);
                float Vx = Vo * Mathf.Cos(angle);

                //get a height and thus time to peak
                float H = -Y + (Vy * Vy) / (2 * GRAVITY);
                float upt = Vy / GRAVITY;                      // time to max height

                //if again we squirt on a neg, but it is because our angle and force
                //are too accute. note: we handle up and down trajectory differently
                if (2 * H / GRAVITY < 0)
                {
                    if (endPoint.y < startPoint.y)
                        rng = flatdistance;       //if going down we are done
                    else
                        rng = 0;       //if up we are not
                }
                else
                {
                    sqrtcheck = 2 * H / GRAVITY;
                    if (sqrtcheck < 0)
                        f = f;
                    else
                    {
                        float dnt = Mathf.Sqrt(sqrtcheck);           // time from max height to impact
                        rng = Vx * (upt + dnt);
                    }
                }

                if (rng > flatdistance)
                    trydistance -= (rng - flatdistance) / 2;		//using a binary zero-in, it takes about 8 iterations to arrive at target
                else if (rng < flatdistance)
                    trydistance += (flatdistance - rng) / 2;

            }
            else
            {
                iters = 64;
            }
            iters++;
        }

        Vector3 angV = p2; //p2 was our flat direction vector above

        //we need to rotate that by our actual launch angle
        angV = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, player.transform.right) * angV;
        


        /*
        //convert the launch angle into a 2d vector (here we need an yz vector)
        float Vyy = Mathf.Sin(angle);
        float Vz = Mathf.Cos(angle);

        Vector3 angV = Vector3.zero;
        angV += new Vector3(0, Vyy, Vz);
        angV.Normalize();



        //get world XY vector to target into a rotation matrix (colums oh woe is me :( )
        //we use this to rotate the 2d trajectory into our world coordinate system
        P1.y = 0;
        Vector3 fwd = P1.normalized;
        Matrix4x4 rotation ;


        Vector3 newRight = fwd.UnitCross(NiPoint3::UNIT_Z);
        Vector3 newUp = newRight.UnitCross(fwd);
        rotation = Matrix4x4.(newRight, fwd, newUp);



        //VERY IMPORTANT: multiplier order is CRITICAL when rotating a vector by a matrix
        angV = rotation* angV;       //and we HAVE to use operator overload because there is no function gamebryo

        */
        Vector3 ret = angV * Vo;   //multiply by calculated "powder charge"   


        return Vo;

    }
}
