using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetGround : MonoBehaviour
{
    public IKTarget target;
    public Image reticule;
    Color prevColor;
    Vector3 prevPoint;

   

    // Start is called before the first frame update
    void Start()
    {
        prevColor = reticule.color;
        prevPoint = target.transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = 1 << 8;
        RaycastHit hit;

        reticule.color = prevColor;
        target.targetPoint= prevPoint;  //the default LOCAL position 
                                        //as child of player

        if (Physics.Raycast(transform.position, transform.forward, out hit, 20.0f, mask))
        {
            //make public the normal of the current polygon I am standing on
            //hitNormal = hit.normal;

            //we need to transform world point to local point so we can
            //return the arm to a local default position
            target.targetPoint = target.transform.worldToLocalMatrix.MultiplyPoint(hit.point); //set world position if in range, as local matrix
            reticule.color = Color.red;

        }
        else
        {
            prevColor = reticule.color;

        }
    }
}
