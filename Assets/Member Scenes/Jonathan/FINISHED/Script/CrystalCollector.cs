using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCollector : MonoBehaviour
{
    private UIManager _uiManager;
    public AudioSource collectSound;


    private void Start()
    {
        _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>(); // This caches the UI Manager so we can reference the script variables
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            _uiManager.crystalsCollected++; // Adds to our counter in UI Manager
            Destroy(gameObject); // Destroy's the object - worth to note that if it was a bigger project object pooling is king.
            collectSound.Play();
        }

        
        //if(other.CompareTag("Player")) This is faster due to not needing to check another GameObject for its tag it simply compares the current trigger's tag with a string
        //if(other.gameObject.tag == "Player")
    }
}
