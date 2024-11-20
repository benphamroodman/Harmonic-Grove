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
    public AudioClip plantSound; // 音效片段
    private AudioSource plantAudioSource; // 音效播放器

    void Start() {
        // 添加一個 AudioSource 給這個物件
        plantAudioSource = gameObject.AddComponent<AudioSource>();

        // 設定音效片段
        if (plantSound != null) {
            plantAudioSource.clip = plantSound;
        }

        // 確保這個 AudioSource 的設置不影響其他音頻
        plantAudioSource.spatialBlend = 1.0f; // 3D 音效
        plantAudioSource.playOnAwake = false; // 不在啟動時播放
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wand")) // 確認碰撞的物件
        {
            // 使用 PlayOneShot 播放音效，避免影響其他音頻
            plantAudioSource.PlayOneShot(plantSound);

            Debug.Log("Plant sound played.");
        }
    }
}