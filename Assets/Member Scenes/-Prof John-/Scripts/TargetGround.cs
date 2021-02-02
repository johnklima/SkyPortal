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
        prevPoint = target.transform.position;
        
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

            if(Input.GetKey(KeyCode.Mouse0))
            {
                target.targetPoint = hit.point;
                prevPoint = hit.point;
            }
                 



            reticule.color = Color.red;

        }
        else
        {
            prevColor = reticule.color;
            prevPoint = target.targetPoint;
        }
    }
}
