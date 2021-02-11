using UnityEngine;

public class GrappleImpactSounds : MonoBehaviour
{
    public static GrappleImpactSounds instance;
    private GameObject player;
    private PlayerScript ps;
    public AudioSource audioS;
    public AudioClip[] clips;
    public AudioClip audioMiss;
    public bool hasAudioPlayed;

    private void Start()
    {
        player = transform.root.gameObject;

        if (player)
        {
            ps = player.GetComponent<PlayerScript>();
        }

        instance = this;
    }
    
    public void PlayGrappleHitSound()
    {
        if (!hasAudioPlayed)
        {
            int rand = Random.Range(0, 2);
            audioS.clip = clips[rand];
            audioS.Play();
            hasAudioPlayed = true;
        }
        
    }

    public void PlayGrappleMissSound()
    {
        audioS.clip = audioMiss;
        audioS.Play();
 
    }

    public void HasLetGoOfGrapple()
    {
        hasAudioPlayed = false;
    }
}
