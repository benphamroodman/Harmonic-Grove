using UnityEngine;

public class FacesUser : MonoBehaviour
{
    // Reference to the target object (the object you want to face)
    [SerializeField] private Transform targetObject;

    void Start()
    {
        // If targetObject is not assigned in the Inspector, find it by name
        if (targetObject == null)
        {
            GameObject foundTarget = GameObject.Find("CenterEyeAnchor");
            if (foundTarget != null)
            {
                targetObject = foundTarget.transform;
                Debug.Log("[FacesUser] Target object assigned to '[BuildingBlock] Camera Rig'.");
            }
            else
            {
                Debug.LogWarning("[FacesUser] '[BuildingBlock] Camera Rig' not found in the scene.");
            }
        }
    }

    void Update()
    {
        // Ensure the targetObject is assigned
        if (targetObject != null)
        {
            // Get the direction from the current object to the target object
            Vector3 direction = targetObject.position - transform.position;

            // Set the y-axis of the direction to 0 to keep the rotation only on the y-axis
            direction.y = 0;

            // If direction is not zero (to avoid errors when the target is directly at the same position)
            if (direction.magnitude > 0)
            {
                // If the object is named "triangle", rotate it 90 degrees on the x-axis as well (so it looks sideways)
                if (transform.name.ToLower().Contains("triangle") || transform.name.ToLower().Contains("Triangle"))
                {
                    transform.Rotate(90, 0, -90, Space.World);  // Rotate 90 degrees on the x-axis
                }   

                // Rotate the object to face the target's position
                transform.rotation = Quaternion.LookRotation(direction);
                
                // Rotate an additional 90 degrees clockwise (on the y-axis)
                transform.Rotate(0, 90, 0, Space.World);
            }
        }
        else
        {
            Debug.LogWarning("[FacesUser] Target object is still not assigned.");
        }
    }
}
