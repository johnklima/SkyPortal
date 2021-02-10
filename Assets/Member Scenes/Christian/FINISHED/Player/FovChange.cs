using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovChange : MonoBehaviour
{
    private Camera mainCamera;
    private PlayerScript _playerScript;
    private float currentFov;
    public float fovChangeSpeed = 4f;
    [Header("Standard Min Fov in Unity is 60")]
    public float minFov = 60f;
    [Header("This is the Fov We Go To When Running")]
    public float maxFov = 85f;
    
    [Header("Hover Over Variable For More Info")]
    [Tooltip("When jumping it will take a x amount of time before resetting the fov (Dramatic Effect)")]
    public float resetTime = 0.3f;
    [Header("The Timer Only For (Debug Purposes)")]
    public float timerReadOnly = 0.3f;
    public AudioSource ac;
    public float volumeIncreaseSpeed = 0.05f;
    private void Start()
    {
        timerReadOnly = resetTime;
        mainCamera = gameObject.GetComponent<Camera>();
        _playerScript = transform.root.GetComponent<PlayerScript>();
        currentFov = mainCamera.fieldOfView;

    }

    private void Update()
    {
        currentFov = mainCamera.fieldOfView;
        
        switch (_playerScript.isRunning && _playerScript.isGrounded && _playerScript.inputDir.magnitude > 0)
        {
            case true:
                mainCamera.fieldOfView = Mathf.Lerp(currentFov, maxFov, fovChangeSpeed * Time.deltaTime);
                break;
            
            case false:
                // if(_playerScript.isGrounded)
                // {mainCamera.fieldOfView = Mathf.Lerp(currentFov, minFov, fovChangeSpeed * Time.deltaTime);}
                // else
                // {
                //     if (timerReadOnly <= 0)
                //     {
                //         mainCamera.fieldOfView = Mathf.Lerp(currentFov, minFov, fovChangeSpeed * Time.deltaTime);
                //     }
                // }
                if (timerReadOnly <= 0)
                {
                    mainCamera.fieldOfView = Mathf.Lerp(currentFov, minFov, fovChangeSpeed * Time.deltaTime);
                }
                break;
        }

        if (_playerScript.isGrounded && _playerScript.isRunning)
        {
            timerReadOnly = resetTime;
        }
        else
        {
            if (timerReadOnly > 0)
            {
                timerReadOnly -= Time.deltaTime;
            }
        }

        if (mainCamera.fieldOfView > 75)
        {
            if (ac.volume < 0.5)

            {
                ac.volume += Time.deltaTime * volumeIncreaseSpeed;
            }
        }
        else
        {
            if(!_playerScript.isUsingJetpack)
            ac.volume -= Time.deltaTime * volumeIncreaseSpeed;
        }

    }
}
