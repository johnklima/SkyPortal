using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{

    public Vector3 targetPoint; 
    public IKSystem iksystem;
    public bool caught = false;
    public bool isThrown = false;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isThrown)
            return;


        transform.position = Vector3.Slerp(transform.position, targetPoint, Time.deltaTime * 2);

        float scale = Vector3.Distance(transform.position, transform.parent.position);
        scale = scale / iksystem.childcount;  //we have 20 links in total

        iksystem.length = scale; //of each link

        if (Vector3.Distance(transform.position, targetPoint) < 2 )
        { 
            
            //iksystem.dragmode = true;
            //iksystem.reachmode = false;
            caught = true;
            
        }            
        else
        {
            //iksystem.dragmode = false;
            //iksystem.reachmode = true;
            caught = false; 
        }
            
    }
}
