using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSegment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void drag(Vector3 target)
    {
        //simply looks at target
        transform.LookAt(target);       


    }


}
