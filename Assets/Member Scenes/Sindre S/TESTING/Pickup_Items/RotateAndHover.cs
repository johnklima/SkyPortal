using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndHover : MonoBehaviour
{
    
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation = OBJVector + v;
        //transform.Rotate(Vector3.forward * rotspeed);

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * 4 * frequency) * amplitude;

        transform.position = tempPos;

    }
}
    