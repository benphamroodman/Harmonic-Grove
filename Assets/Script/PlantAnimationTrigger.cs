using UnityEngine;

public class PlantAnimationTrigger : MonoBehaviour {
    private Animator animator;
    private bool isActive = false;  // 篈ち传北ガ狶

    void Start() {
        animator = GetComponent<Animator>();  // 莉Animator舱ン
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wand"))  // 浪琩窱疾琌wand
        {
            isActive = !isActive;  // ち传篈
            animator.SetBool("isActive", isActive);  // 穝笆礶把计

            Debug.Log(isActive ? "play animation" : "back to Idle state");
        }
    }
}
