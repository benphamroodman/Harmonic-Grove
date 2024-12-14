//using UnityEngine;

//public class PlantAnimationTrigger : MonoBehaviour {
//    private Animator animator;
//    private bool isActive = false;  // ���A��������L

//    void Start() {
//        animator = GetComponent<Animator>();  // ���Animator�ե�
//    }

//    private void OnTriggerEnter(Collider other) {
//        if (other.CompareTag("wand"))  // �ˬd�I�����O�_��wand
//        {
//            isActive = !isActive;  // �������A
//            animator.SetBool("isActive", isActive);  // ��s�ʵe�Ѽ�

//            Debug.Log(isActive ? "play animation" : "back to Idle state");
//        }
//    }
//}
using UnityEngine;

public class PlantAnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private bool isActive = false;  // ���A��������L
    public ParticleSystem particleSystem;  // �ޥβɤl�t��

    void Start()
    {
        animator = GetComponent<Animator>();  // ���Animator�ե�

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem is not assigned in the inspector!");
        }
        else
        {
            particleSystem.Stop();  // ��l�Ʈ������ɤl�t��
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wand"))  // �ˬd�I�����O�_��wand
        {
            isActive = !isActive;  // �������A
            animator.SetBool("isActive", isActive);  // ��s�ʵe�Ѽ�

            if (isActive)
            {
                particleSystem?.Play();  // �Ұʲɤl�t��
                Debug.Log("play animation and particles");
            }
            else
            {
                particleSystem?.Stop();  // ����ɤl�t��
                Debug.Log("back to Idle state and stop particles");
            }
        }
    }
}

