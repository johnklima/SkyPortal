using System.Collections;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public GameObject player;
    private PlayerScript ps;
    public AudioClip[] clips;
    public AudioSource ac;
    private bool couroutineOn;
    private float stepDelay;
    public float walkDelay;
    public float runDelay;
    public float countDown;
    public float stopWalkSoundSpeed;


    private void Start()
    {
        int rand = Random.Range(0, 1);
        ps = player.GetComponent<PlayerScript>();
        couroutineOn = true;
        ac.clip = clips[rand];

        StartCoroutine(Walking());
    }

    private void Update()
    {
        if (ps.isGrounded)
        {
            countDown = 1f;
        }
        else
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime * stopWalkSoundSpeed;
            }
        }

        if (countDown < 0)
        {
            ac.Stop();
        }
    }

    IEnumerator Walking()
    {

        while (couroutineOn)
        {

            if (ps.inputDir.magnitude > 0 && countDown > 0)
            {
                int rand = Random.Range(0, 1);
                
                if (ps.isRunning)
                {
                    stepDelay = runDelay;
                    ac.volume = Random.Range(0.9f, 1f);
                    ac.pitch = Random.Range(1f, 1.2f);
                    ac.clip = clips[rand];
                }
                else
                {
                    stepDelay = walkDelay;
                    ac.volume = Random.Range(0.8f, 1f);
                    ac.pitch = Random.Range(0.8f, 1f);
                    ac.clip = clips[rand];
                    
                }

                ac.Play();
                
            }
            
            yield return new WaitForSeconds(stepDelay);
            
        }
    }

}