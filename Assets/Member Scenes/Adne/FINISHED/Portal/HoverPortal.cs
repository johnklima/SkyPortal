using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPortal : MonoBehaviour
{
    public Vector3 stratPosition;
    public float frequency = 1f;
    public float magnitude = 1f;
    public float offset = 0f;
    // Start is called before the first frame update
    void Start()
    {
    stratPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = stratPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
}
