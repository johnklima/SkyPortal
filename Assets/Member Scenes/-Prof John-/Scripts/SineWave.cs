using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{

    public float freq = 1;
    public float ampl = 1;
    public Vector3 startPos;
    float sin = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //remove previous value
        transform.position -= new Vector3(0, sin, 0);
        //get sine now
        sin = Mathf.Sin(Time.time * freq) * ampl;
        //add new value
        transform.position += new Vector3(0, sin, 0);
        
    }
}
