using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    public bool isKnocking;
    public int material;
    public float pitch;
    public int BPM;

    // You can expose this in the Unity Inspector to test different time windows
    [SerializeField]
    private float timeWindow = 5f;
    // Store the previous state to detect transitions
    private bool previousSignalState = false;
    // Counter for the number of transitions
    private int transitionCount = 0;
    // Timer to track the 5-second window
    private double timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool startKnocking = false;
        if (isKnocking)
        {
            startKnocking = true;
        }

        if (startKnocking)
        {
            // Increment the timer
            timer += Time.deltaTime;
        }

        // Check for false to true transition
        if (isKnocking && !previousSignalState)
        {
            transitionCount++;
            Debug.Log($"Transition detected! Count: {transitionCount}");
        }

        // Store current state for next frame's comparison
        previousSignalState = isKnocking;

        // Check if we've reached the time window
        if (timer >= timeWindow)
        {
            Debug.Log($"Time window ended. Final count: {transitionCount}");
            BPM = transitionCount;

            // Reset counter and timer for the next window
            transitionCount = 0;
            timer = 0f;
            startKnocking = false;
        }
    }
}
