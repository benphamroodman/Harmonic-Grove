using System.Collections;
using UnityEngine;

public class MoveGuitarOnCollision : MonoBehaviour
{
    // This method will be called when the object collides with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the tag "wand"
        if (other.CompareTag("wand"))
        {
            // Find the child named "PlantAppearPlace (10)"
            Transform plantAppearPlace = transform.Find("PlantAppearPlace (10)");

            if (plantAppearPlace != null)
            {
                // Find the child of "PlantAppearPlace (10)" named "guitar_ani"
                Transform guitarAni = plantAppearPlace.Find("guitar_ani");

                if (guitarAni != null)
                {
                    // Start the coroutine to move the guitar gradually
                    StartCoroutine(MoveGuitarToPosition(guitarAni, new Vector3(0, 2, 0), 1f));
                }
                else
                {
                    Debug.LogError("Child 'guitar_ani' not found in 'PlantAppearPlace (10)'.");
                }
            }
            else
            {
                Debug.LogError("Child 'PlantAppearPlace (10)' not found.");
            }
        }
    }

    // Coroutine to smoothly move the guitar to a target position over a set duration
    private IEnumerator MoveGuitarToPosition(Transform guitarAni, Vector3 targetPosition, float duration)
    {
        Debug.Log("Moving guitar to position!");
        
        Vector3 startPosition = guitarAni.localPosition;  // Starting position of the guitar
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate how much time has passed and interpolate between start and target positions
            guitarAni.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

            timeElapsed += Time.deltaTime;  // Increase the time elapsed by the frame time
            yield return null;  // Wait for the next frame
        }

        // Ensure that the final position is exactly the target position
        guitarAni.localPosition = targetPosition;
    }
}
