using UnityEngine;

public class HideMeshOnKeyPress : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // �ˬd�O�_���U��L H ��
        if (Input.GetKeyDown(KeyCode.H))
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                // ���� MeshRenderer ���ҥΪ��A
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
