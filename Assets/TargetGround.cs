using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetGround : MonoBehaviour
{

    public Image reticule;
    Color prevColor;

    // Start is called before the first frame update
    void Start()
    {
        prevColor = reticule.color;
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = 1 << 8;
        RaycastHit hit;

        reticule.color = prevColor;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 20.0f, mask))
        {
            //make public the normal of the current polygon I am standing on
            //hitNormal = hit.normal;

            reticule.color = Color.red;

        }
        else
        {
            prevColor = reticule.color;
        }
    }
}
