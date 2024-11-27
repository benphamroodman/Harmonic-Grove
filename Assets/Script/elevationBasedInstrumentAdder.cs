using UnityEngine;

public class ElevationBasedInstrumentAdder : MonoBehaviour
{
    // Variable to track the last known elevation range
    private string lastElevationRange;

    /**void Start()
    {
        // Initialize lastElevationRange based on the current position
        UpdateElevationRange();
    }**/

    void Update()
    {
        // Check if the elevation range has changed, and if so, call the function
        CheckElevationAndAddInstrument();
    }

    // This method checks the current elevation and adds the instrument if it has changed range
    private void CheckElevationAndAddInstrument()
    {
        float yPosition = transform.position.y;

        string currentElevationRange = GetElevationRange(yPosition);

        // Check if the elevation range has changed since the last frame
        if (currentElevationRange != lastElevationRange)
        {
            // Call AddInstrument with the new parameters
            AddInstrument(currentElevationRange);
            // Update the last known elevation range
            lastElevationRange = currentElevationRange;
        }
    }

    // Determines which range the elevation (Y position) is in
    private string GetElevationRange(float yPosition)
    {
        if (yPosition < 5f)
        {
            Debug.Log("Low Elevation");
            return "Low Elevation";
        }
        else if (yPosition >= 5f && yPosition <= 8f)
        {
            Debug.Log("Medium Elevation");
            return "Medium Elevation";
        }
        else
        {
            Debug.Log("High Elevation");
            return "High Elevation";
        }
    }

    // This method will be called with parameters based on the current elevation range
    private void AddInstrument(string elevationRange)
    {
        int level = 0;
        if (elevationRange == "Low Elevation")
        {
            level = 1;
        }
        else if (elevationRange == "Medium Elevation")
        {
            level = 2;
        }
        else if (elevationRange == "High Elevation")
        {
            level = 3;
        }

        // Example logging, or replace with actual instrument adding logic
        Debug.Log($"Added {elevationRange} instrument with level {level}");
    }
}
