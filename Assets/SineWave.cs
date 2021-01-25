using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{

    public float freq = 1;
    public float ampl = 1;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = startPos;
        float s = Mathf.Sin(Time.time * freq) * ampl;
        transform.position = new Vector3(transform.position.x, s, transform.position.z);
        //startPos = new Vector3(transform.position.x, transform.position.y - s, transform.position.z);
    }
}
