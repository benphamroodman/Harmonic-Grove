using UnityEngine;

public class ExitOnEsc : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 检查是否按下了 Esc 键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 退出应用程序
            Application.Quit();

            // 在编辑器中无法退出程序，所以我们添加一个额外的调试语句
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}