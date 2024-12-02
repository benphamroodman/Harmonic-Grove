using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProcGenMusic
{
    public class NewAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private MusicGenerator mMusicGenerator;

        [SerializeField]
        private int[] mInstrumentIndices;

        [SerializeField]
        private List<int> beatsToPlayOn = new List<int>();  // List of beats (1-16) on which to play notes

        private NewInstrumentHandler[] newInstrumentHandlers;

        private bool isAudioPlayerActive = true;  // Boolean to track if the audio player is active
        private bool isColliding = false;  // Flag to track whether the wand is currently colliding with the instrument
        private int currentBeat = 1;  // Current beat in the sequence (1-16)
        private float timeSinceLastBeat = 0f;  // Time tracker for beat interval (1/8th of a second)
        private float beatInterval = 0.125f;  // Interval for beat checking (1/8th of a second)

        void Start()
        {
            Debug.Log("Starting audio player!");
            newInstrumentHandlers = new NewInstrumentHandler[mInstrumentIndices.Length];
            for (var index = 0; index < newInstrumentHandlers.Length; index++)
            {
                newInstrumentHandlers[index] = new NewInstrumentHandler();
                newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[index]);
            }
        }

        void Update()
        {
            timeSinceLastBeat += Time.deltaTime;

            // Every 1/8th of a second, check if the beat has updated
            if (timeSinceLastBeat >= beatInterval)
            {
                timeSinceLastBeat -= beatInterval;
                Debug.Log("Beat has updated!");

                // Check if the audio player is active and track beats
                if (isAudioPlayerActive)
                {
                    Debug.Log("Audio player is active!");
                    // Check if the current beat is in the list of beats to play on
                    if (beatsToPlayOn.Contains(currentBeat))
                    {
                        Debug.Log("About to play the beat!");
                        PlayNoteOnCurrentBeat();
                    }
                }

                // Update the beat, loop back to 1 after 16
                currentBeat++;
                if (currentBeat > 16)
                {
                    currentBeat = 1;
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wand") && !isColliding)
            {
                // Toggle the audio player on/off on collision
                isAudioPlayerActive = !isAudioPlayerActive;
                Debug.Log(isAudioPlayerActive ? "Audio player ON." : "Audio player OFF.");

                // Set the flag to true to prevent repeated collisions
                isColliding = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("wand"))
            {
                // Reset the collision flag when the wand exits the instrument collider
                isColliding = false;
            }
        }

        // Play the note for the current instrument handler
        private void PlayNoteOnCurrentBeat()
        {
            // Play note for the first instrument handler (modify as needed for others)
            Debug.Log("Now I'm really about to play the note!");
            newInstrumentHandlers[0].PlayNote();
            Debug.Log("Played note on beat: " + currentBeat);
        }
    }
}
