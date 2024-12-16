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
    void Start()
    {
        // 每10秒自動調用一次 spawnStringPlant 方法
        spawnTrianglePlant();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drum"))
        {
            Renderer drumRenderer = other.gameObject.GetComponent<Renderer>();
            drumRenderer.material.color = Color.black;
            spawnDrumPlant();
        }
        else if (other.CompareTag("Guitar"))
        {
            Renderer drumRenderer = other.gameObject.GetComponent<Renderer>();
            drumRenderer.material.color = Color.black;
            spawnGuitarPlant();
        }
        else if (other.CompareTag("Triangle"))
        {
            Renderer drumRenderer = other.gameObject.GetComponent<Renderer>();
            drumRenderer.material.color = Color.black;
            spawnTrianglePlant();
        }
        else if (other.CompareTag("String"))
        {
            Renderer drumRenderer = other.gameObject.GetComponent<Renderer>();
            drumRenderer.material.color = Color.black;
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
