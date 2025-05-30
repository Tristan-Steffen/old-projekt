using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    // Audio clip to play when the object collides with something
    public AudioClip collisionSound;

    // Audio source to play the collision sound
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the audio source component on this game object
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnCollisionEnter is called when this object collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "deko" tag
        if (collision.gameObject.tag == "deko")
        {
            // Play the collision sound
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
