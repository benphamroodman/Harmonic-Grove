using UnityEngine;
//using Oculus.Interaction; // 引入 Meta Tools Building Blocks 的命名空間
using Oculus.Interaction.HandGrab; // HandGrabInteractable 類別通常在這裡
using Oculus.Interaction;          // Meta 基本互動命名空間


public class DisableHandGrab : MonoBehaviour
{
    // 設定一個可供拖拉的 GameObject 陣列
    public GameObject[] handGrabObjects;

    // 方法：停用所有 HandGrab 功能
    public void DisableHandGrabComponents()
    {
        foreach (GameObject obj in handGrabObjects)
        {
            var handGrabInteractable = obj.GetComponent<HandGrabInteractable>();
            if (handGrabInteractable != null)
            {
                handGrabInteractable.enabled = false; // 停用 HandGrab 組件
            }
        }
    }

    // 方法：啟用所有 HandGrab 功能（如果需要恢復）
    public void EnableHandGrabComponents()
    {
        foreach (GameObject obj in handGrabObjects)
        {
            var handGrabInteractable = obj.GetComponent<HandGrabInteractable>();
            if (handGrabInteractable != null)
            {
                handGrabInteractable.enabled = true; // 啟用 HandGrab 組件
            }
        }
    }
}
