using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Portal_Controller : MonoBehaviour
{
    private GameObject player;
    public Collider player_collider;
    public GameObject connected_portal;
    public float test;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPS_Player");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider == player_collider)
        {
            test += 1;
            player.transform.position = new Vector3(connected_portal.transform.position.x, connected_portal.transform.position.y, connected_portal.transform.position.z);
        }
    }
}
