using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProcGenMusic
{
    public class NewerAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private MusicGenerator mMusicGenerator;

        [SerializeField]
        private int[] mInstrumentIndices;

        private NewInstrumentHandler newInstrumentHandler;  // Single instrument handler

        [SerializeField]
        private List<int> beatsToPlayOn = new List<int>();  // List of beats (1-16) on which to play notes

        public TempoTracker tempoTracker;  // Reference to the TempoTracker

        private bool isAudioPlayerActive = false;  // Boolean to track if the audio player is active
        private bool isColliding = false;  // Flag to track whether the wand is currently colliding with the instrument

        private bool hasPlayedNote = false;  // Boolean to track if the note has been played for the current beat
        private int lastCheckedBeat = -1;  // Track the last checked beat to compare for changes

        private int randomInstrumentIndex;
        private int randomBeatIndex;

        void Start()
        {
            // Select a random instrument index between 0 and 3 (inclusive)
            randomInstrumentIndex = Random.Range(0, 4);  // Random int between 0 and 3

            // Initialize the instrument handler with the randomly selected instrument index
            newInstrumentHandler = new NewInstrumentHandler();
            newInstrumentHandler.Initialize(mMusicGenerator, mInstrumentIndices[randomInstrumentIndex]);

            determineBeatsToPlayOn();
        }

        void determineBeatsToPlayOn()
        {
            // Create the four sets of beats
            List<List<int>> beatSets = new List<List<int>>()
            {
                new List<int>{ 3, 4, 7, 11, 12, 15 },
                new List<int>{ 3, 7, 11, 15, 16 },
                new List<int>{ 1, 3, 5, 6, 9, 11, 13, 15 },
                new List<int>{ 1, 2, 3, 4, 9, 10, 11, 12 }
            };

            // Select a random index from the list of sets
            randomBeatIndex = Random.Range(0, beatSets.Count);

            // Assign the randomly selected set to beatsToPlayOn
            beatsToPlayOn = beatSets[randomBeatIndex];

            // Debugging output to verify the result
            Debug.Log("Beats to play on: " + string.Join(", ", beatsToPlayOn));
        }

        void Update()
        {
            // Check if the audio player is active and track beats from the TempoTracker
            if (isAudioPlayerActive)
            {
                // Use the currentBeat from TempoTracker
                int currentBeat = tempoTracker.currentBeat;

                // If the beat has changed since the last time, reset the played note flag
                if (currentBeat != lastCheckedBeat)
                {
                    hasPlayedNote = false;  // Reset for the new beat
                    lastCheckedBeat = currentBeat;  // Update the last checked beat
                }

                // Check if the current beat is in the list of beats to play on and if the note hasn't been played yet
                if (beatsToPlayOn.Contains(currentBeat) && !hasPlayedNote)
                {
                    PlayNoteOnCurrentBeat(currentBeat);
                    hasPlayedNote = true;  // Mark that the note has been played for this beat
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wand") && !isColliding)
            {
                Debug.Log("Audio player is toggled!");
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
        private void PlayNoteOnCurrentBeat(int currentBeat)
        {
            // Play the note for the selected instrument handler
            newInstrumentHandler.PlayNote();

            Debug.Log("Played note on beat: " + currentBeat + " for instrument: " + randomInstrumentIndex);
        }
    }
}
