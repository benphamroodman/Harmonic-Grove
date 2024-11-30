using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGeneratingContoller : MonoBehaviour
{
    public GameObject flowerPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pencil"))
        {
            Debug.Log("Collide with pencil!");
            Vector3 spawnPosition = new Vector3(-4.85f, 0.8f, 4f);
            Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
