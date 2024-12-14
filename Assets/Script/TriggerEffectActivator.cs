using UnityEngine;

public class TriggerEffectActivator : MonoBehaviour
{
    private GameObject effect; // �l����ޥ�

    private void Start()
    {
        // �M��l���� "effect"
        effect = transform.Find("effect")?.gameObject;

        if (effect == null)
        {
            Debug.LogError("Child object 'effect' not found!");
        }
        else
        {
            effect.SetActive(false); // ��l�Ʈ�����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�I��������O�_���S�w���� "wand"
        if (other.CompareTag("wand"))
        {
            if (effect != null)
            {
                // ���� effect ���ҥΪ��A
                bool currentState = effect.activeSelf;
                effect.SetActive(!currentState);

                Debug.Log($"Effect is now {(currentState ? "Deactivated" : "Activated")}");
            }
        }
    }
}
