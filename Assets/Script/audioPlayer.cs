using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip plantSound; // Public field for the jump sound effect
    private AudioSource plantAudioSource; // For the plant sound

    void Start()
    {
        plantAudioSource = gameObject.AddComponent<AudioSource>(); // Create a new AudioSource 

        // Assign the jump sound effect if it was set in the Inspector
        if (plantSound != null)
        {
            plantAudioSource.clip = plantSound;
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien")) // Check if the plant collided with the alien
        {
            plantAudioSource.Play();
            
        }
        Debug.Log("plant touch");
    }
}
