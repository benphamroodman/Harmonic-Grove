using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlantSpawner : MonoBehaviour
{
    public GameObject[] drumPrefabs;
    public GameObject[] guitarPrefabs;
    public GameObject[] trianglePrefabs;
    public GameObject[] stringPrefabs;
    public GameObject drumArea;
    public GameObject guitarArea;
    public GameObject triangleArea;
    public GameObject stringArea;
    public float heightOffset = 0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drum"))
        {
            spawnDrumPlant();
        }
        else if (other.CompareTag("Guitar"))
        {
            spawnGuitarPlant();
        }
        else if (other.CompareTag("Triangle"))
        {
            spawnTrianglePlant();
        }
        else if (other.CompareTag("String"))
        {
            spawnStringPlant();
        }
    }

    void spawnDrumPlant()
    {
        int randomIndex = Random.Range(0, drumPrefabs.Length);
        GameObject selectedPlantPrefab = drumPrefabs[randomIndex];
        Renderer areaRenderer = drumArea.GetComponent<Renderer>();

        Vector3 areaCenter = areaRenderer.bounds.center;
        Vector3 areaSize = areaRenderer.bounds.size;

        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        float randomY = areaCenter.y + heightOffset;

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
        Instantiate(selectedPlantPrefab, spawnPosition, Quaternion.identity);
    }

    void spawnGuitarPlant()
    {
        int randomIndex = Random.Range(0, guitarPrefabs.Length);
        GameObject selectedPlantPrefab = guitarPrefabs[randomIndex];
        Renderer areaRenderer = guitarArea.GetComponent<Renderer>();

        Vector3 areaCenter = areaRenderer.bounds.center;
        Vector3 areaSize = areaRenderer.bounds.size;

        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        float randomY = areaCenter.y + heightOffset;

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
        Instantiate(selectedPlantPrefab, spawnPosition, Quaternion.identity);
    }

    void spawnTrianglePlant()
    {
        int randomIndex = Random.Range(0, trianglePrefabs.Length);
        GameObject selectedPlantPrefab = trianglePrefabs[randomIndex];
        Vector3 areaCenter = triangleArea.transform.position;
        float randomX = Random.Range(areaCenter.x - 0.3f, areaCenter.x + 0.3f);
        float randomZ = Random.Range(areaCenter.z - 0.3f, areaCenter.z + 0.3f);
        float randomY = areaCenter.y + heightOffset;
        Vector3 spawnPosition = new Vector3(areaCenter.x, areaCenter.y, areaCenter.z);
        Debug.Log("Spawn Position: " + spawnPosition);
        GameObject spawnedPlant = Instantiate(selectedPlantPrefab, spawnPosition, Quaternion.identity);
    }
    void spawnStringPlant()
    {
        int randomIndex = Random.Range(0, stringPrefabs.Length);
        GameObject selectedPlantPrefab = stringPrefabs[randomIndex];
        Renderer areaRenderer = stringArea.GetComponent<Renderer>();

        Vector3 areaCenter = areaRenderer.bounds.center;
        Vector3 areaSize = areaRenderer.bounds.size;

        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        float randomY = areaCenter.y + heightOffset;

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
        Instantiate(selectedPlantPrefab, spawnPosition, Quaternion.identity);
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject[] drumPrefabs;
    public GameObject[] guitarPrefabs;
    public GameObject[] trianglePrefabs;
    public GameObject[] stringPrefabs;

    public GameObject drumArea;
    public GameObject guitarArea;
    public GameObject triangleArea;
    public GameObject stringArea;

    public GameObject plantAppearPlace_2; // �ͦ���m���ѦҪ���
    public float heightOffset = 0f; // ���װ���

    void Start()
    {
        if (plantAppearPlace_2 == null)
        {
            Debug.LogError("PlantAppearPlace_2 is not assigned!");
        }
        else
        {
            // �ոծɪ�l�ƥͦ��Ӫ�
            spawnPlant(drumPrefabs);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (plantAppearPlace_2 == null)
        {
            Debug.LogError("PlantAppearPlace_2 is not assigned!");
            return;
        }

        Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();

        if (other.CompareTag("Drum"))
        {
            objectRenderer.material.color = Color.black;
            spawnPlant(drumPrefabs);
        }
        else if (other.CompareTag("Guitar"))
        {
            objectRenderer.material.color = Color.black;
            spawnPlant(guitarPrefabs);
        }
        else if (other.CompareTag("Triangle"))
        {
            objectRenderer.material.color = Color.black;
            spawnPlant(trianglePrefabs);
        }
        else if (other.CompareTag("String"))
        {
            objectRenderer.material.color = Color.black;
            spawnPlant(stringPrefabs);
        }
    }

    void spawnPlant(GameObject[] prefabs)
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("No prefabs available for spawning!");
            return;
        }

        // �H������Ӫ��w�m��
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPlantPrefab = prefabs[randomIndex];

        // ��� PlantAppearPlace_2 ���@�ɦ�m
        Vector3 spawnPosition = plantAppearPlace_2.transform.position + new Vector3(0, heightOffset, 0);

        // ��ҤƴӪ�
        GameObject spawnedPlant = Instantiate(selectedPlantPrefab, spawnPosition, Quaternion.identity);

        // �]�m�� PlantAppearPlace_2 ���l����
        spawnedPlant.transform.SetParent(plantAppearPlace_2.transform);

        // ���m�۹��m�� (0, 0, 0)
        spawnedPlant.transform.localPosition = Vector3.zero;

        // �ոիH��
        Debug.Log($"Spawned Plant at World Position: {spawnedPlant.transform.position}");
        Debug.Log($"Spawned Plant Local Position: {spawnedPlant.transform.localPosition}");
    }
}

*/