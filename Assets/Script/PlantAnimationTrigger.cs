//using UnityEngine;

//public class PlantAnimationTrigger : MonoBehaviour {
//    private Animator animator;
//    private bool isActive = false;  // 篈ち传北ガ狶

//    void Start() {
//        animator = GetComponent<Animator>();  // 莉Animator舱ン
//    }

//    private void OnTriggerEnter(Collider other) {
//        if (other.CompareTag("wand"))  // 浪琩窱疾琌wand
//        {
//            isActive = !isActive;  // ち传篈
//            animator.SetBool("isActive", isActive);  // 穝笆礶把计

//            Debug.Log(isActive ? "play animation" : "back to Idle state");
//        }
//    }
//}
using UnityEngine;

public class PlantAnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private bool isActive = false;  // 篈ち传北ガ狶
    public ParticleSystem particleSystem;  // まノ采╰参

    void Start()
    {
        animator = GetComponent<Animator>();  // 莉Animator舱ン

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem is not assigned in the inspector!");
        }
        else
        {
            particleSystem.Stop();  // ﹍て闽超采╰参
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wand"))  // 浪琩窱疾琌wand
        {
            isActive = !isActive;  // ち传篈
            animator.SetBool("isActive", isActive);  // 穝笆礶把计

            if (isActive)
            {
                particleSystem?.Play();  // 币笆采╰参
                Debug.Log("play animation and particles");
            }
            else
            {
                particleSystem?.Stop();  // 氨ゎ采╰参
                Debug.Log("back to Idle state and stop particles");
            }
        }
    }
}

