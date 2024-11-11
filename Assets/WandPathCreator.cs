using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPathCreator : MonoBehaviour
{
    public GameObject pathPrefab; // Assign the path segment prefab in the Inspector
    public float segmentSpacing = 0.5f; // Distance between each path segment

    private Vector3 lastPosition;

    void Start() {
        lastPosition = transform.position;
    }

    void Update() {
        if (Vector3.Distance(transform.position, lastPosition) >= segmentSpacing) {
            Instantiate(pathPrefab, transform.position, Quaternion.identity);
            lastPosition = transform.position;
        }
    }
}
