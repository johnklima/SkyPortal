using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSound : MonoBehaviour
{
    public GameObject player;
    private PlayerScript ps;

    public AudioSource[] ac;

    public bool jetpackStarting;
    public bool jetpackSustaining;
    public bool jetpackEnding;

    private void Start()
    {
        ps = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // First we need to see if we are using jetpack
        if (ps.isUsingJetpack && !ps.isGrappled)
        {
            if (!jetpackStarting && !jetpackSustaining)
            {
                ac[0].Play();
                jetpackStarting = true;
            }

            if (jetpackStarting && !ac[0].isPlaying) // Then we check if start sound is finished, if it is then we play next sound

            {
                if (!jetpackSustaining)
                {
                    ac[1].Play();
                    jetpackSustaining = true;
                    
                }
            }
        }
        
        // if we were using the jetpack and now we've stopped we need play jetpack off sound
        
        if (!ps.isUsingJetpack && jetpackStarting && !jetpackEnding && !ps.isGrappled || !ps.isUsingJetpack && jetpackSustaining && !jetpackEnding && !ps.isGrappled) 
        {
            if (!jetpackEnding)
            {
                ac[0].Stop();
                ac[1].Stop();
                ac[2].Play();
                jetpackEnding = true;

            }
        }
        
        // Reset

        if (!ps.isUsingJetpack && jetpackEnding)
        {
            jetpackStarting = false;
            jetpackSustaining = false;
            jetpackEnding = false;
        }
        
        
    }
}
