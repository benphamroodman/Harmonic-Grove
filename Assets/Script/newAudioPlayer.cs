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

        public TempoTracker tempoTracker;  // Reference to the TempoTracker

        private NewInstrumentHandler[] newInstrumentHandlers;

        private bool isAudioPlayerActive = false;  // Boolean to track if the audio player is active
        private bool isColliding = false;  // Flag to track whether the wand is currently colliding with the instrument

        private bool hasPlayedNote = false;  // Boolean to track if the note has been played for the current beat
        private int lastCheckedBeat = -1;  // Track the last checked beat to compare for changes

        void Start()
        {
            newInstrumentHandlers = new NewInstrumentHandler[mInstrumentIndices.Length];
            for (var index = 0; index < newInstrumentHandlers.Length; index++)
            {
                newInstrumentHandlers[index] = new NewInstrumentHandler();
                newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[index]);
            }
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
            // Get the y-position of the parent object
            float yPosition = transform.position.y;

            // Determine which instrument handler to play based on the y-position
            int instrumentIndex = 0;

            if (yPosition >= 1f && yPosition <= 2f)
            {
                instrumentIndex = 1;  // Select the second instrument handler if the y-value is between 1 and 2
            }
            else if (yPosition > 2f)
            {
                instrumentIndex = 2;  // Select the third instrument handler if the y-value is above 2
            }

            // Play the note for the selected instrument handler
            newInstrumentHandlers[instrumentIndex].PlayNote();

            Debug.Log("Played note on beat: " + currentBeat + " for instrument: " + instrumentIndex);
        }
    }
}
