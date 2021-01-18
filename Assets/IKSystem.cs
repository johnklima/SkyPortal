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

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastSegment.drag(target.position);
    }
}
