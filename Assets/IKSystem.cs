using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour
{
    public IKSegment[] segments;
    public Transform target = null;

    private IKSegment lastSegment = null;
    private IKSegment firstSegment = null;


    int childcount;
    void Awake()    {

        //lets buffer our segements in an array
        childcount = transform.childCount;
        segments = new IKSegment[childcount];
        int i = 0;
        foreach (Transform seg in transform)
        {
            segments[i] = seg.GetComponent<IKSegment>();
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


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if you want simple drag, uncomment below, and comment reach
        //lastSegment.drag(target.position);

        //call reach on the last
        lastSegment.reach(target.position);

        //and forward update on the first
        //we needed to maintain that first segment original position
        //which is the position of the IK system itself

        //COMMENT NEXT LINE AND IK ROOT WILL FOLLLOW TARGET:
        firstSegment.transform.position = transform.position;

        firstSegment.updateSegmentAndChildren();

    }
}
