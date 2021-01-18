using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IKSegment : MonoBehaviour
{

    public float length = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void drag(Vector3 target)
    {
        //simply looks at target
        transform.LookAt(target);

        //next move too
        transform.position = target - transform.forward * length;
    }


}
