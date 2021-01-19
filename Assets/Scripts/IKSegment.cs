using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IKSegment : MonoBehaviour
{

    public float length = 3.0f;
    public IKSegment parent = null;
    public IKSegment child = null;
    public IKSystem system = null;

    public Vector3 Apos = new Vector3(0, 0, 0);
    public Vector3 Bpos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void drag(Vector3 target)
    {
        //simply looks at target
        transform.LookAt(target);

        //next move to
        transform.position = target - transform.forward * length;

        //inform my parent
        if (parent)
            parent.drag(transform.position);
    }
    public void reach(Vector3 target)
    {
        drag(target);
        updateSegment();
    }

    public void updateSegmentAndChildren()
    {

        updateSegment();

        //update its children
        if (child)
            child.updateSegmentAndChildren();
    }

    public void updateSegment() 
    {
        if (parent)
        {
            Apos = parent.Bpos;         //could also use parent endpoint...
            transform.position = Apos;  //move me to Bpos (parent endpoint)
        }
        else
        {
            //Apos is always my position
            transform.position = system.transform.position;
            Apos = transform.position;
        }
        //Bpos is always where the endpoint will be, as calculated from length 
        calculateBpos();

    }
    void calculateBpos()
    {
        Bpos = Apos + transform.forward * length;
    }
}
