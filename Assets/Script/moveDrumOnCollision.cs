using System.Collections;
using UnityEngine;

public class MoveDrumOnCollision : MonoBehaviour
{
    // This method will be called when the object collides with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the tag "wand"
        if (other.CompareTag("wand"))
        {
            // Find the child named "PlantAppearPlace (2)"
            Transform plantAppearPlace = transform.Find("PlantAppearPlace (2)");

            if (plantAppearPlace != null)
            {
                // Find the child of "PlantAppearPlace (2)" named "drum_v2_ani"
                Transform drumAni = plantAppearPlace.Find("drum_v2_ani");

                if (drumAni != null)
                {
                    // Start the coroutine to move the drum gradually
                    StartCoroutine(MoveDrumToPosition(drumAni, new Vector3(0, 2, 0), 1f));
                }
                else
                {
                    Debug.LogError("Child 'drum_v2_ani' not found in 'PlantAppearPlace (2)'.");
                }
            }
            else
            {
                Debug.LogError("Child 'PlantAppearPlace (2)' not found.");
            }
        }
    }

    // Coroutine to smoothly move the drum to a target position over a set duration
    private IEnumerator MoveDrumToPosition(Transform drumAni, Vector3 targetPosition, float duration)
    {
        Debug.Log("Moving drum to position!");
        
        Vector3 startPosition = drumAni.localPosition;  // Starting position of the drum
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate how much time has passed and interpolate between start and target positions
            drumAni.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

            timeElapsed += Time.deltaTime;  // Increase the time elapsed by the frame time
            yield return null;  // Wait for the next frame
        }

        // Ensure that the final position is exactly the target position
        drumAni.localPosition = targetPosition;
    }
}
