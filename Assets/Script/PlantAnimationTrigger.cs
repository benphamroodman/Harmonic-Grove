using UnityEngine;

public class PlantAnimationTrigger : MonoBehaviour {
    private Animator animator;
    private bool isActive = false;  // ���A��������L

    void Start() {
        animator = GetComponent<Animator>();  // ���Animator�ե�
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wand"))  // �ˬd�I�����O�_��wand
        {
            isActive = !isActive;  // �������A
            animator.SetBool("isActive", isActive);  // ��s�ʵe�Ѽ�

            Debug.Log(isActive ? "play animation" : "back to Idle state");
        }
    }
}
