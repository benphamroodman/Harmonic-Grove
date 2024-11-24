using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
public class AudioPlayer : MonoBehaviour
{
    public AudioClip plantSound; // Public field for the jump sound effect
    private AudioSource plantAudioSource; // For the plant sound

    void Start()
    {
        plantAudioSource = gameObject.AddComponent<AudioSource>(); // Create a new AudioSource 

        // Assign the jump sound effect if it was set in the Inspector
        if (plantSound != null)
        {
            plantAudioSource.clip = plantSound;
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wand")) // Check if the plant collided with the alien
        {
            plantAudioSource.Play();
            
        }
        Debug.Log("plant touch");
    }
}
*/

public class AudioPlayer : MonoBehaviour {
    public AudioClip plantSound; // ���Ĥ��q
    private AudioSource plantAudioSource; // ���ļ���

    void Start() {
        // �K�[�@�� AudioSource ���o�Ӫ���
        plantAudioSource = gameObject.AddComponent<AudioSource>();

        // �]�w���Ĥ��q
        if (plantSound != null) {
            plantAudioSource.clip = plantSound;
        }

        // �T�O�o�� AudioSource ���]�m���v�T��L���W
        plantAudioSource.spatialBlend = 1.0f; // 3D ����
        plantAudioSource.playOnAwake = false; // ���b�Ұʮɼ���
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wand")) // �T�{�I��������
        {
            // �ϥ� PlayOneShot ���񭵮ġA�קK�v�T��L���W
            plantAudioSource.PlayOneShot(plantSound);

            Debug.Log("Plant sound played.");
        }
    }
}