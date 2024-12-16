using UnityEngine;

public class FacesUser : MonoBehaviour
{
    // Declare a public Transform variable to reference the camera's parent object (Camera Rig)
    public Transform cameraRig;

    void Start()
    {
        // Optionally, automatically assign the Camera Rig if not set in the Inspector
        if (cameraRig == null)
        {
            // Find the Camera Rig GameObject by its name and access its Transform
            cameraRig = GameObject.Find("[BuildingBlock] Camera Rig").transform;  // Use your exact object name here
        }
    }

    void Update()
    {
        // Ensure the object faces the camera rig
        if (cameraRig != null)
        {
            // Get the position of the camera in the camera rig
            Transform cameraTransform = cameraRig.Find("Camera");  // Assuming the camera is a child of Camera Rig
            if (cameraTransform != null)
            {
                // Make the object face the camera (while maintaining its current y-axis orientation)
                Vector3 targetPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
                transform.LookAt(targetPosition);
            }
            else
            {
                Debug.LogError("Camera not found under Camera Rig.");
            }
        }
        else
        {
            Debug.LogError("Camera Rig not found.");
        }
    }
}
