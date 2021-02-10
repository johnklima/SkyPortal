using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetGround : MonoBehaviour
{
    public IKTarget target;
    public IKSystem system;

    public Image reticule;
    Color prevColor;
    Vector3 prevPoint;
    Vector3 startPos;

   

    // Start is called before the first frame update
    void Start()
    {
        prevColor = reticule.color;
        prevPoint = startPos = target.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = 1 << 8;
        RaycastHit hit;

        reticule.color = prevColor;
        target.targetPoint= prevPoint;  //the default LOCAL position 
                                        //as child of player


        //disengage on klickright
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            system.hide();
            target.reset();



        }

        float dist = system.childcount - 0.05f; // how many unit length links in our chain
        if (Physics.Raycast(transform.position, transform.forward, out hit, dist, mask))
        {
            //make public the normal of the current polygon I am standing on
            //hitNormal = hit.normal;

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                target.isThrown = true;
                target.targetPoint = hit.point + transform.forward * 10 + Vector3.up * 10;
                prevPoint = hit.point;
                system.show();
                
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
