using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{

    public Vector3 targetPoint; //IN LOCAL SPACE

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPoint, Time.deltaTime * 2);
    }
}
