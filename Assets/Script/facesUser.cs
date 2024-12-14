using UnityEngine;

public class FacesUser : MonoBehaviour
{
    // Reference to the camera (user's viewpoint)
    [SerializeField] private Transform userCamera;

    void Start()
    {
        // Optionally, automatically assign the camera if not set in the Inspector
        if (userCamera == null)
        {
            userCamera = Camera.main.transform;  // Use the main camera if no reference is set
        }
    }

    void Update()
    {
        // Make the object face the camera (user) while maintaining its current y-axis orientation (if desired)
        Vector3 targetPosition = new Vector3(userCamera.position.x, transform.position.y, userCamera.position.z);
        transform.LookAt(targetPosition);
    }
}
