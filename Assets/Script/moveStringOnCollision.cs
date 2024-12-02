using System.Collections;
using UnityEngine;

public class MoveStringOnCollision : MonoBehaviour
{
    // This method will be called when the object collides with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the tag "wand"
        if (other.CompareTag("wand"))
        {
            // Find the child named "PlantAppearPlace (6)"
            Transform plantAppearPlace = transform.Find("PlantAppearPlace (6)");

            if (plantAppearPlace != null)
            {
                // Find the child of "PlantAppearPlace (6)" named "string_ani"
                Transform stringAni = plantAppearPlace.Find("string_ani");

                if (stringAni != null)
                {
                    // Start the coroutine to move the string gradually
                    StartCoroutine(MoveStringToPosition(stringAni, new Vector3(0, 2, 0), 1f));
                }
                else
                {
                    Debug.LogError("Child 'string_ani' not found in 'PlantAppearPlace (6)'.");
                }
            }
            else
            {
                Debug.LogError("Child 'PlantAppearPlace (6)' not found.");
            }
        }
    }

    // Coroutine to smoothly move the string to a target position over a set duration
    private IEnumerator MoveStringToPosition(Transform stringAni, Vector3 targetPosition, float duration)
    {
        Debug.Log("Moving string to position!");
        
        Vector3 startPosition = stringAni.localPosition;  // Starting position of the string
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate how much time has passed and interpolate between start and target positions
            stringAni.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

            timeElapsed += Time.deltaTime;  // Increase the time elapsed by the frame time
            yield return null;  // Wait for the next frame
        }

        // Ensure that the final position is exactly the target position
        stringAni.localPosition = targetPosition;
    }
}
