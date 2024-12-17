using UnityEngine;
//using Oculus.Interaction; // �ޤJ Meta Tools Building Blocks ���R�W�Ŷ�
using Oculus.Interaction.HandGrab; // HandGrabInteractable ���O�q�`�b�o��
using Oculus.Interaction;          // Meta �򥻤��ʩR�W�Ŷ�


public class DisableHandGrab : MonoBehaviour
{
    // �]�w�@�ӥi�ѩ�Ԫ� GameObject �}�C
    public GameObject[] handGrabObjects;

    // ��k�G���ΩҦ� HandGrab �\��
    public void DisableHandGrabComponents()
    {
        foreach (GameObject obj in handGrabObjects)
        {
            var handGrabInteractable = obj.GetComponent<HandGrabInteractable>();
            if (handGrabInteractable != null)
            {
                handGrabInteractable.enabled = false; // ���� HandGrab �ե�
            }
        }
    }

    // ��k�G�ҥΩҦ� HandGrab �\��]�p�G�ݭn��_�^
    public void EnableHandGrabComponents()
    {
        foreach (GameObject obj in handGrabObjects)
        {
            var handGrabInteractable = obj.GetComponent<HandGrabInteractable>();
            if (handGrabInteractable != null)
            {
                handGrabInteractable.enabled = true; // �ҥ� HandGrab �ե�
            }
        }
    }
}
