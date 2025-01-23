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
    public enum PlantTypes { Background_Noise, glass , metal , soft , wood }
    public int SpawnCount = 5;
    static int RandomPointsCount = 10;
	public float spawnInterval = 1f; // �ͦ����j�ɶ��]��^

    public bool CheetMode = false;
    

	#region Singleton

	public static PlantSpawner instance;

	private void Awake()
	{
        instance = this;
	}

	#endregion

	#region Trigger Func

    /// <summary>
    /// old version
    /// </summary>
    /// <param name="other"></param>
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

	public void spawnPlant(PlantTypes PlantType, GameObject PlaneObj)
	{
        List<Vector3> RandomPoints = GenerateRandomPointsOnPlane(PlaneObj, RandomPointsCount);
        List<Vector3> SelectPoints = SelectFarthestPoints(RandomPoints, SpawnCount);

		if (PlantType == PlantTypes.soft) //  "Drum"
		{
            StartSpawning(SelectPoints, SpawnCount, drumPrefabs, PlaneObj.transform);
			//spawnDrumPlant();
		}
		else if (PlantType == PlantTypes.wood) // "Guitar"
		{
			StartSpawning(SelectPoints, SpawnCount, guitarPrefabs, PlaneObj.transform);
			//spawnGuitarPlant();
		}
		else if (PlantType == PlantTypes.metal) // "Triangle"
		{
			StartSpawning(SelectPoints, SpawnCount, trianglePrefabs, PlaneObj.transform);
			//spawnTrianglePlant();
		}
		else if (PlantType == PlantTypes.glass) // "String"
		{
			StartSpawning(SelectPoints, SpawnCount, stringPrefabs, PlaneObj.transform);
			//spawnStringPlant();
		}
	}
	#endregion

	#region Random Position

	// �b�������ͦ��H���I
	List<Vector3> GenerateRandomPointsOnPlane(GameObject plane, int count)
	{
		List<Vector3> points = new List<Vector3>();
		BoxCollider boxCollider = plane.GetComponent<BoxCollider>();
		Bounds bounds = boxCollider.bounds;

		for (int i = 0; i < count; i++)
		{
			// �b������ɤ��H���ͦ� x �M z �y��
			float x = Random.Range(bounds.min.x, bounds.max.x);
			float z = Random.Range(bounds.min.z, bounds.max.z);

			// �T�w y �b��m���������� -> localpos.y = 0
			//Vector3 randomPoint = new Vector3(x, plane.transform.position.y, z);
			Vector3 randomPoint = new Vector3(x, 0f, z);
			points.Add(randomPoint);
		}

		return points;
	}

	// �q�H���I���D��Z���̻����I
	List<Vector3> SelectFarthestPoints(List<Vector3> points, int count)
	{
		List<Vector3> selectedPoints = new List<Vector3>();

		if (points.Count == 0 || count <= 0)
			return selectedPoints;

		// �H����ܲĤ@���I
		selectedPoints.Add(points[Random.Range(0, points.Count)]);

		while (selectedPoints.Count < count)
		{
			Vector3 farthestPoint = Vector3.zero;
			float maxDistance = float.MinValue;

			// �M��Z���ثe�襤�I�̻����I
			foreach (Vector3 point in points)
			{
				if (selectedPoints.Contains(point))
					continue;

				float totalDistance = 0f;
				foreach (Vector3 selected in selectedPoints)
				{
					totalDistance += Vector3.Distance(point, selected);
				}

				if (totalDistance > maxDistance)
				{
					maxDistance = totalDistance;
					farthestPoint = point;
				}
			}

			selectedPoints.Add(farthestPoint);
		}

		return selectedPoints;
	}


	#endregion

	#region Spawn Func

	/// <summary>
	/// �ھګ��w����m�C��M�ͦ����ƥͦ�����C
	/// </summary>
	/// <param name="positions">�@�է�����m</param>
	/// <param name="count">�n�ͦ�������</param>
	public void StartSpawning(List<Vector3> positions, int count, GameObject[] PrefabArr, Transform PlaneTransform)
	{
        //SystemController.instance.FloorMappingBuildDebugTexts[3].text = "StartSpawning with count : " + count;
		StartCoroutine(SpawnObjectsCoroutine(positions, count, PrefabArr, PlaneTransform));
	}

	// �ϥΨ�{����ͦ��L�{
	private IEnumerator SpawnObjectsCoroutine(List<Vector3> positions, int count, GameObject[] PrefabArr, Transform PlaneTransform)
	{
		int positionIndex = 0;

		for (int i = 0; i < count; i++)
		{
			// �T�O���ަb�d�򤺴`��
			if (positions.Count > 0)
			{
				int randomIndex = Random.Range(0, PrefabArr.Length);
				GameObject selectedPlantPrefab = PrefabArr[randomIndex];
				Vector3 spawnPosition = positions[positionIndex % positions.Count];
				SpawnObject(spawnPosition, selectedPlantPrefab, PlaneTransform);
				SystemController.instance.FloorMappingBuildDebugTexts[3].text += "\n SpawnObject count : " + (positionIndex + 1);

				// ���W���ޥH�`���ϥΦ�m
				positionIndex++;
			}

			// ���ݫ��w�ɶ�
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	// �ͦ����󪺤�k
	private void SpawnObject(Vector3 localPosition, GameObject PlantPrefab, Transform PlaneTransform)
	{

		if (PlantPrefab == null)
		{
			Debug.LogError("Prefab is not assigned!");
			return;
		}
        /*
        GameObject spawnedObject = Instantiate(PlantPrefab, PlaneTransform);
		spawnedObject.transform.position = localPosition;
		spawnedObject.transform.rotation = Quaternion.identity;
        */


		// �ͦ�����A�]�m����e���󪺤l����A�ó]�w������m
		
		GameObject spawnedObject = Instantiate(PlantPrefab, PlaneTransform);
		spawnedObject.transform.localPosition = localPosition;
        spawnedObject.transform.rotation = Quaternion.identity;
        spawnedObject.transform.parent = null;
        //spawnedObject.transform.localScale = Vector3.one;
		
		//Debug.Log($"Spawned object at local position: {localPosition}");
	}

    // old version below

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
        Renderer areaRenderer = triangleArea.GetComponent<Renderer>();
        Vector3 areaCenter = areaRenderer.bounds.center;
        Vector3 areaSize = areaRenderer.bounds.size;
        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        float randomY = areaCenter.y + heightOffset;
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
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

	#endregion
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