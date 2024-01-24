using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerHover: MonoBehaviour
{

    private float degreesPerSecond;
    private float amplitude;
    private float frequency;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        degreesPerSecond = Random.Range(5f, 30f);
        amplitude = Random.Range(0.03f, 0.09f);
        frequency = Random.Range(0.2f, 0.8f);
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
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

    }

}
