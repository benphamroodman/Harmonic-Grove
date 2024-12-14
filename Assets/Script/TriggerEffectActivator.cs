using UnityEngine;

public class TriggerEffectActivator : MonoBehaviour
{
    private GameObject effect; // 子物件引用

    private void Start()
    {
        // 尋找子物件 "effect"
        effect = transform.Find("effect")?.gameObject;

        if (effect == null)
        {
            Debug.LogError("Child object 'effect' not found!");
        }
        else
        {
            effect.SetActive(false); // 初始化時關閉
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞的物件是否有特定標籤 "wand"
        if (other.CompareTag("wand"))
        {
            if (effect != null)
            {
                // 切換 effect 的啟用狀態
                bool currentState = effect.activeSelf;
                effect.SetActive(!currentState);

                Debug.Log($"Effect is now {(currentState ? "Deactivated" : "Activated")}");
            }
        }
    }
}
