using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool isOnGround = false;
    public bool isAtWall = false;

    gravity grav;

    // Start is called before the first frame update
    void Start()
    {
        grav = GetComponent<gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        float fwd = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        bool strafe = Input.GetKey(KeyCode.RightShift);


        //ground movement
        if(isOnGround)
        {
            if(!strafe)
                transform.Rotate(transform.up * turn * Time.deltaTime * 100);
            

            if (!isAtWall )
            {
                if(!strafe)
                    transform.position += transform.forward * fwd * Time.deltaTime * 10;
                else
                    transform.position += transform.right * turn * Time.deltaTime * 10;

            }
            
        }


    }
}
