using System.Collections.Generic;
using UnityEngine;

public class AlienFollower : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalThreshold = 0.1f;

    private List<Transform> pathSegments = new List<Transform>();
    private int currentSegmentIndex = 0;

    void Start() {
        RefreshPathSegments();
    }

    void Update() {
        if (pathSegments.Count == 0) {
            Debug.LogWarning("No path segments found!");
            return;
        }

        if (currentSegmentIndex >= pathSegments.Count) {
            Debug.Log("Reached end of path. Resetting to start.");
            currentSegmentIndex = 0;  // Loop back to start
            return;
        }

        Transform targetSegment = pathSegments[currentSegmentIndex];

        // Calculate distance before moving
        float distanceToTarget = Vector3.Distance(transform.position, targetSegment.position);

        // Check if we've reached the current segment
        if (distanceToTarget < arrivalThreshold) {
            Debug.Log($"Reached segment {currentSegmentIndex}. Moving to next segment.");
            currentSegmentIndex++;

            // If we still have segments left, get the next target
            if (currentSegmentIndex < pathSegments.Count) {
                targetSegment = pathSegments[currentSegmentIndex];
                Debug.Log($"New target segment: {currentSegmentIndex} at position {targetSegment.position}");
            }
            return;  // Skip movement this frame to prevent overshooting
        }

        // Move towards the current segment
        Vector3 direction = (targetSegment.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face the direction of movement
        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Visual debug info
        Debug.DrawLine(transform.position, targetSegment.position, Color.red);
        Debug.Log($"Moving to segment {currentSegmentIndex} | Distance: {distanceToTarget:F2} | Target: {targetSegment.position}");
    }

    public void RefreshPathSegments() {
        pathSegments.Clear();
        GameObject[] segments = GameObject.FindGameObjectsWithTag("PathSegment");

        Debug.Log($"Found {segments.Length} path segments:");
        foreach (GameObject segment in segments) {
            pathSegments.Add(segment.transform);
            Debug.Log($"- {segment.name} at position {segment.transform.position}");
        }

        currentSegmentIndex = 0;
    }

    // Visual debug in Scene view
    private void OnDrawGizmos() {
        if (pathSegments == null || pathSegments.Count == 0) return;

        // Draw the full path
        Gizmos.color = Color.green;
        for (int i = 0; i < pathSegments.Count - 1; i++) {
            if (pathSegments[i] != null && pathSegments[i + 1] != null) {
                Gizmos.DrawLine(pathSegments[i].position, pathSegments[i + 1].position);
                Gizmos.DrawWireSphere(pathSegments[i].position, arrivalThreshold);
            }
        }

        // Draw the last point
        if (pathSegments.Count > 0 && pathSegments[pathSegments.Count - 1] != null) {
            Gizmos.DrawWireSphere(pathSegments[pathSegments.Count - 1].position, arrivalThreshold);
        }

        // Highlight current target
        if (Application.isPlaying && currentSegmentIndex < pathSegments.Count) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pathSegments[currentSegmentIndex].position, arrivalThreshold * 1.5f);
        }
    }
}