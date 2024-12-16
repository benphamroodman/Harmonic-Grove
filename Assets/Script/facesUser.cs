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
            // Manually assign the AR Camera or use the AR system camera
            userCamera = Camera.main?.transform ?? ARCamera.transform;  // Use the AR camera if no main camera
        }
    }

    void LateUpdate()
    {
        // Make the object face the camera (user) while maintaining its current y-axis orientation (if desired)
        Vector3 targetPosition = new Vector3(userCamera.position.x, transform.position.y, userCamera.position.z);
        transform.LookAt(targetPosition);
    }
}
