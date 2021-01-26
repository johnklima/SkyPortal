using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fwd = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Rotate(transform.up * turn * Time.deltaTime * 100);
        transform.position += transform.forward * fwd * Time.deltaTime * 10;
    
    
    }
}
