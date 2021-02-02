using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_timer : MonoBehaviour
{
    public float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
