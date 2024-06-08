using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip collisionClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionClip;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Furniture" tag
        if (collision.gameObject.tag.Contains("Furniture-"))
        {
            // Play the collision sound
            audioSource.Play();
        }
    }
}
