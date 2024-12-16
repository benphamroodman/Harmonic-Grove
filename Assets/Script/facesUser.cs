using UnityEngine;

public class FacesUser : MonoBehaviour
{
    // Reference to the camera (user's viewpoint)
    // [SerializeField] 
    private Transform userCamera;

    void Start()
    {
        // Optionally, if the userCamera isn't set in the Inspector, you can find the camera by name
        if (userCamera == null)
        {
            userCamera = GameObject.Find("[BuildingBlock] Camera Rig");  // Replace "ARCamera" with your camera's name
        }
    }

    void Update()
    {
        // Make the object face the camera (user) while maintaining its current y-axis orientation (if desired)
        Vector3 targetPosition = new Vector3(userCamera.position.x, transform.position.y, userCamera.position.z);
        transform.LookAt(targetPosition);
    }
}
