using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{

    public Vector3 targetPoint; //IN LOCAL SPACE
    public IKSystem iksystem;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPoint, Time.deltaTime * 2);

        float scale = Vector3.Distance(transform.position, transform.parent.position);
        scale = scale / 20.0f;

        iksystem.length = scale; //of each link

    }
}
