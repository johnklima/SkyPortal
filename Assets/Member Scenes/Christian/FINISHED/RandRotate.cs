using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandRotate : MonoBehaviour
{
    void Start()
    {
        int rand = Random.Range(-360, 360);

        transform.rotation = Quaternion.Euler(0,rand,0);
    }
}
