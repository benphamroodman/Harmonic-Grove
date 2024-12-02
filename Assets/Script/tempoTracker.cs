using UnityEngine;

public class TempoTracker : MonoBehaviour
{
    public float bpm = 120f;  // Beats per minute, adjust this to your desired tempo
    private float secondsPerBeat;  // Time in seconds for each beat
    private float timeSinceLastBeat;  // Time passed since the last beat
    private int currentBeat;  // Current beat in the sequence (1 to 16)

    private float beatInterval = 0.125f;  // Interval in seconds (1/8th second)

    void Start()
    {
        // Calculate how long each beat lasts in seconds
        secondsPerBeat = 60f / bpm;
        currentBeat = 1;
    }

    void Update()
    {
        // Track time passed since the last beat
        timeSinceLastBeat += Time.deltaTime;

        // If 1/8th of a second has passed, print the current beat
        if (timeSinceLastBeat >= beatInterval)
        {
            // Print the current beat to the console
            // Debug.Log("Current Beat: " + currentBeat);

            // Increment the beat, loop back to 1 after 16 beats
            currentBeat++;
            if (currentBeat > 16)
            {
                currentBeat = 1;
            }

            // Reset the timer for the next beat interval
            timeSinceLastBeat -= beatInterval;
        }
    }
}
