using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour
{
    public IKSegment[] segments;
    public IKTarget target = null;

    private IKSegment lastSegment = null;
    private IKSegment firstSegment = null;

    public PlayerScript player;

    public float length = 1; //applied to geom scale
    public bool dragmode = false;
    public bool reachmode = true;

    public int childcount;

    bool isHidden = true;

    void Awake()    {

        //lets buffer our segements in an array
        childcount = transform.childCount;
        segments = new IKSegment[childcount];
        int i = 0;
        foreach (Transform seg in transform)
        {
            segments[i] = seg.GetComponent<IKSegment>();
            segments[i].system = this;
            i++;
        }

        firstSegment = segments[0];
        lastSegment = segments[childcount - 1];

        for(int c = 1; c < childcount; c++)
        {
            segments[c].parent = segments[c - 1];

            if (c < childcount - 2)
            {
                segments[c].child = segments[c + 1];
            }

        }
        //now assign c == 0
        segments[0].child = segments[1];

        hide(); //the links

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden)
            return;

        for (int i = 0; i < childcount; i++)
        {

            Vector3 scale = segments[i].transform.GetChild(0).localScale;
            scale.z = length;
            segments[i].transform.GetChild(0).localScale = scale;//.Set(scale.x,scale.y,scale.z);

        }


        //if you want simple drag
        if (dragmode && !reachmode)
        {
            lastSegment.drag(target.transform.position);
        }

        //call reach on the last
        if (!dragmode && reachmode)
        {
            lastSegment.reach(target.transform.position);
            
            //and forward update on the first
            //we needed to maintain that first segment original position
            //which is the position of the IK system itself

            //COMMENT NEXT LINE AND IK ROOT WILL FOLLLOW TARGET:
            firstSegment.transform.position = transform.position;
            firstSegment.transform.rotation = transform.parent.rotation;
            firstSegment.updateSegmentAndChildren();
        }
        
        //potential transition state between modes
        if (dragmode && reachmode)
        {

        }



    }
    public void hide() 
    {
        for (int i = 0; i < childcount; i++)
        {
            segments[i].transform.position = Vector3.zero;
        }
        isHidden = true;
    }
    public void show()
    {
        for (int i = 0; i < childcount; i++)
        {
            //maybe something here?
            
        }
        isHidden = false;

    }
}
