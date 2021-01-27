using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeom : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;        
        transform.localPosition = new Vector3(0,1,0);       // i can do this because my guy is a unit.
    }
}
