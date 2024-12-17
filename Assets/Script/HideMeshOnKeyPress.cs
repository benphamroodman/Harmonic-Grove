using UnityEngine;

public class HideMeshOnKeyPress : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ÀË¬d¬O§_«ö¤UÁä½L H Áä
        if (Input.GetKeyDown(KeyCode.H))
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                // ¤Á´« MeshRenderer ªº±Ò¥Îª¬ºA
                meshRenderer.enabled = !meshRenderer.enabled;
                Debug.Log($"MeshRenderer is now {(meshRenderer.enabled ? "enabled" : "disabled")}");
            }
            else
            {
                Debug.LogWarning("No MeshRenderer component found on this GameObject!");
            }
        }
    }
}
