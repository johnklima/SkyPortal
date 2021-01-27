using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    gravity grav;

    // Player Specs
    public float walkSpeed;
    public float runSpeed;
    public float minDist;
    public float jumpHeight;
    
    // Jetpack
    public float boostAmount = 0.05f;
    public float fuel = 10f;
    public float maxFuel = 10f;
    public float rechargeAmount = 2f;
    public float fuelUsage = 0.1f;

    public float timerBeforeJetPack = 0.4f;
    [ReadOnly]
    private float timerForJetPackReadOnly = 0.4f;
    
    
    // Checks
    public bool isGrounded;
    public bool isJumping;
    public bool isUsingJetpack = false;
    public bool isRunning;

    public bool isAtWall;

    public float gravconstant = -9.8f;

    // Start is called before the first frame update
    private void Start()
    {
        grav = transform.GetComponent<gravity>();
    }
    // Update is called once per frame
    void Update()
    {
        //isRunning = Input.GetKey(KeyCode.LeftShift); // Allows for a very cool high skill mechanic double jump && Adjust falling speed mid air
        
        isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded; // Does not allow the above && makes run only function when grounded

        // Checks if grounded and if we are not we apply Gravity
        if (!isGrounded)
        {

        }
        else
        {
            // Else we want to check if we are jumping, if we are not then set velocity to 0 to prevent falling through the floor
            if (!isJumping)
            {
                
            }
        }
        
        // Move inputs
        
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        
        // Inputs combined with velocity to apply the gravity
        Vector3 move = new Vector3(inputDir.x, grav.velocity.y, inputDir.y);

        // Checking if we are running as we'll adjust speed depending on true || false
        if (isRunning && !isAtWall)
        {
            if (!isUsingJetpack)
            {
                transform.Translate(move * runSpeed * Time.deltaTime);
            }

        }
        else if(!isAtWall && !isRunning)
        {
            transform.Translate(move * walkSpeed * Time.deltaTime);
        }
        
        
        
        // Another check to see if we are grounded to allow jumping (I put it here seperate from the gravity to easier know what each part does... its early ok..)
        if (isGrounded)
        {
            // If we are grounded then isJumping is false
            isJumping = false;
            isUsingJetpack = false;
            timerBeforeJetPack = timerForJetPackReadOnly;
            
            // If isJumping is false then we should be able to jump as we havent used up our jump!
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isAtWall)
            {
                
                // Math stuff to counter act the Gravity and have a smooth jump yayy :)
                //grav.velocity.y += Mathf.Sqrt(-3 * gravconstant * jumpHeight * Time.deltaTime);
                grav.velocity += Vector3.up * 3f;
                
                // Of course we need to tell the script that NOW we are jumping.
                isJumping = true;
            }
        }
        
        // Fires off The Jetpack Function bellow update
        JetPack();

        if (isJumping)
        {
            if (timerBeforeJetPack > 0)
            {
                timerBeforeJetPack -= Time.deltaTime; // Counts downwards to eventually hit 0 where the player is allowed to use the jetpack!
            }
        }
        else
        {
            timerBeforeJetPack = timerForJetPackReadOnly; // I set the timer to a read only value as its never going to change and it helps me store the float.
        }
        
    }
    
    private void JetPack()
    {
        if(isAtWall) return;
        // First we need to check if the player is jumping so they dont overlap, during this we also have a timer for some finesse
        if (isJumping && fuel > 0 && Input.GetKey(KeyCode.Space) && timerBeforeJetPack <= 0)
        {
            isUsingJetpack = true;

            // This just ensures that when isUsingJetpack is true then we actually move upwards
            if (isUsingJetpack)
            {
                grav.velocity.y += boostAmount;

                fuel -= fuelUsage;
            }
            else
            {
                // If we are not using the jetpack then i dont want it to interfere with our velocity anymore.
                grav.velocity.y = 0;
            }

        }
        
        // if fuel is less then or equal to 0 then usingjetpack will be false and you'll be falling
        if (fuel <= 0)
        {
            isUsingJetpack = false;
            timerBeforeJetPack = timerForJetPackReadOnly;

        }

        // If your fuel is less the max and you're not using it then it will recharge
        if (fuel < maxFuel && !isUsingJetpack)
        {
            fuel += rechargeAmount * Time.deltaTime;
            
        }

        // If the player lets go of space while using jetpack then isUsingJetpack will be false to recharge + add a timer before use again of 0.04f or so.
        if (Input.GetKeyUp(KeyCode.Space) && isUsingJetpack)
        {
            isUsingJetpack = false;
            timerBeforeJetPack = timerForJetPackReadOnly;
        }
    }
}
