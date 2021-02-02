using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Portal_controller : MonoBehaviour
{
    private GameObject player;
    private GameObject timer;
    public Collider player_collider;
    public GameObject player_camera;
    public GameObject connected_portal;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPS_Player");
        timer = GameObject.Find("Portal_timer");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (timer.GetComponent<Portal_timer>().timer <= 0)
        {
            if (collider == player_collider)
            {
                timer.GetComponent<Portal_timer>().timer = 5;
                player.transform.position = new Vector3(connected_portal.transform.position.x, connected_portal.transform.position.y, connected_portal.transform.position.z);
                player_camera.transform.rotation = connected_portal.transform.rotation;
            }
        }
        
    }
}
