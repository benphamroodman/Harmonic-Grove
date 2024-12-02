using System.Collections;
using UnityEngine;

public class MoveTriangleOnCollision : MonoBehaviour
{
    // This method will be called when the object collides with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the tag "wand"
        if (other.CompareTag("wand"))
        {
            // Find the child named "PlantAppearPlace (4)"
            Transform plantAppearPlace = transform.Find("PlantAppearPlace (4)");

            if (plantAppearPlace != null)
            {
                // Find the child of "PlantAppearPlace (4)" named "triangle_ani"
                Transform triangleAni = plantAppearPlace.Find("triangle_ani");

                if (triangleAni != null)
                {
                    // Start the coroutine to move the triangle gradually
                    StartCoroutine(MoveTriangleToPosition(triangleAni, new Vector3(0, 2, 0), 1f));
                }
                else
                {
                    Debug.LogError("Child 'triangle_ani' not found in 'PlantAppearPlace (4)'.");
                }
            }
            else
            {
                Debug.LogError("Child 'PlantAppearPlace (4)' not found.");
            }
        }
    }

    // Coroutine to smoothly move the triangle to a target position over a set duration
    private IEnumerator MoveTriangleToPosition(Transform triangleAni, Vector3 targetPosition, float duration)
    {
        Debug.Log("Moving triangle to position!");
        
        Vector3 startPosition = triangleAni.localPosition;  // Starting position of the triangle
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate how much time has passed and interpolate between start and target positions
            triangleAni.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

            timeElapsed += Time.deltaTime;  // Increase the time elapsed by the frame time
            yield return null;  // Wait for the next frame
        }

        // Ensure that the final position is exactly the target position
        triangleAni.localPosition = targetPosition;
    }
}
