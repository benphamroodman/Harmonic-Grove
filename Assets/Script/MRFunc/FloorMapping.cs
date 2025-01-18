using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
//using static Meta.XR.MRUtilityKit.FindSpawnPositions;

/// <summary>
/// put on the object want to map on plane
/// 
/// </summary>
public class FloorMapping : MonoBehaviour
{
	/// <summary>
	/// Defines possible locations where objects can be spawned.
	/// </summary>
	public enum SpawnLocation
	{
		Floating, // Spawn somewhere floating in the free space within the room
		AnySurface, // Spawn on any surface (i.e. a combination of all 3 options below)
		VerticalSurfaces, // Spawn only on vertical surfaces such as walls, windows, wall art, doors, etc...
		OnTopOfSurfaces, // Spawn on surfaces facing upwards such as ground, top of tables, beds, couches, etc...
		HangingDown // Spawn on surfaces facing downwards such as the ceiling
	}
	[FormerlySerializedAs("selectedSnapOption")]
	[SerializeField, Tooltip("Attach content to scene surfaces.")]
	public SpawnLocation SpawnLocations = SpawnLocation.AnySurface;
	[SerializeField, Tooltip("Maximum number of times to attempt spawning/moving an object before giving up.")]
	public int MaxIterations = 1000;
	[SerializeField, Tooltip("When using surface spawning, use this to filter which anchor labels should be included. Eg, spawn only on TABLE or OTHER.")]
	public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels) 0;

	public GameObject MapPosObj;
	public bool IsTestBuild = true;
	float Timer = 5f;

	private void Update()
	{
		if (IsTestBuild)
		{
			Timer -= Time.deltaTime;
			if(Timer <= 0f)
			{
				MapToPlane();
				Timer = 5f;
			}
		}
	}

	Vector3 GetNormal(Vector3 CurClosestPosition, MRUKAnchor anchor)
	{
		Bounds? bounds = anchor.VolumeBounds;
		Vector3 diff = CurClosestPosition - anchor.GetAnchorCenter();
		//Vector3 diff = anchor.transform.InverseTransformPoint(CurClosestPosition) - anchor.transform.position;
		if (bounds != null)
		{
			if(bounds.Value.extents.x == Mathf.Abs(diff.x))
			{
				if(diff.x > 0)
				{
					return Vector3.right;
				}
				else
				{
					return Vector3.left;
				}
			}
			if(bounds.Value.extents.y == Mathf.Abs(diff.y))
			{
				if(diff.y > 0)
				{
					return Vector3.up;
				}
				else
				{
					return Vector3.down;
				}
			}
			if(bounds.Value.extents.z == Mathf.Abs(diff.z))
			{
				if(diff.z > 0)
				{
					return Vector3.forward;
				}
				else
				{
					return Vector3.back;
				}
			}
		}
		return Vector3.zero;
	}

	public void MapToPlane()
	{
		Debug.Log("MapToPlane");
		foreach (var room in MRUK.Instance.Rooms)
		{
			MapToPlane(room);
			//room.AnchorCreatedEvent.AddListener(TryMapToTable);
		}
	}

	public void MapToPlane(MRUKRoom room)
	{
		MRUKAnchor[] RoomAnchors = room.GetComponentsInChildren<MRUKAnchor>();
		MRUKAnchor Best = null;
		if (RoomAnchors.Length > 0)
		{
			float distance = Mathf.Infinity;
			Vector3 closestPosition = Vector3.zero, normal = Vector3.up;
			Vector3 CurClosestPosition = Vector3.zero, CurNormal = Vector3.up;
			foreach (var anchor in RoomAnchors)
			{
				float CurDistance = anchor.GetClosestSurfacePosition(MapPosObj.transform.position, out CurClosestPosition, out CurNormal);
				//Debug.Log("Distance between " + anchor.name + " is : " + CurDistance);
				//CurDistance = anchor.GetDistanceToSurface(MapPosObj.transform.position);
				//Debug.Log("GetDistanceToSurface return: " + CurDistance);
				//float CurDistance = Vector3.Distance(MapPosObj.transform.position, anchor.transform.position);
				if (CurDistance < distance)
				{
					distance = CurDistance;
					Best = anchor;
					closestPosition = CurClosestPosition;
					normal = GetNormal(CurClosestPosition ,anchor);
				}
			}
			float BestDot = Mathf.Infinity;
			//Debug.Log("Use " + Best.name + " as best, closestPosition : " + closestPosition + " ,diff  : " + (closestPosition - Best.GetAnchorCenter()) + " ,anchor : " + Best.GetAnchorCenter() + " ,half size : "+ Best.VolumeBounds.Value.extents + ", normal : " + normal);
			foreach(var pos in Best.GetBoundsFaceCenters())
			{
				float CurDot = Vector3.Dot(pos - Best.GetAnchorCenter(), pos - closestPosition);
				Debug.Log("Bounds Face Centers :" + pos + ", plane normal : " + (pos - Best.GetAnchorCenter()) + ", dot test : " + CurDot);
				if(Vector3.Dot(pos - Best.GetAnchorCenter(), pos - closestPosition) < BestDot)
				{
					normal = pos - Best.GetAnchorCenter();
					BestDot = CurDot;
				}
			}

			transform.position = closestPosition;
			transform.rotation *= Quaternion.FromToRotation(transform.up, normal);
		}
	}


	/// <summary>
	/// Starts the spawning process for all rooms.
	/// </summary>
	public void MapToTable()
	{
		Debug.Log("MapToTable");
		foreach (var room in MRUK.Instance.Rooms)
		{
			MapToTable(room);
			//room.AnchorCreatedEvent.AddListener(TryMapToTable);
		}
	}

	public void MapToTable(MRUKRoom room)
	{
		MRUKAnchor[] RoomAnchors = room.GetComponentsInChildren<MRUKAnchor>();
		MRUKAnchor Best = null;
		if (RoomAnchors.Length > 0)
		{
			float distance = Mathf.Infinity;
			foreach(var anchor in RoomAnchors)
			{
				if(anchor.Label == Labels)
				{
					float CurDistance = Vector3.Distance(MapPosObj.transform.position, anchor.transform.position);
					if(CurDistance < distance)
					{
						distance = CurDistance;
						Best = anchor;
					}
				}
			}
			if(Best == null)
			{
				Debug.LogError("No table");
				return;
			}
			Vector3 TableSize = new Vector3(Best.PlaneBoundary2D[0].x, 0f, Best.PlaneBoundary2D[0].y);
			gameObject.transform.position = Best.transform.position;
			GetComponent<BoxCollider>().size = TableSize;
		}
	}


	/// <summary>
	/// current no use
	/// </summary>
	/// <param name="room"></param>
	public void MapToMRFloor(MRUKRoom room)
    {
		var prefabBounds = Utilities.GetPrefabBounds(gameObject);
		float minRadius = 0.0f;
		const float clearanceDistance = 0.01f;
		float baseOffset = -prefabBounds?.min.y ?? 0.0f;
		float centerOffset = prefabBounds?.center.y ?? 0.0f;

		if (prefabBounds.HasValue)
		{
			minRadius = Mathf.Min(-prefabBounds.Value.min.x, -prefabBounds.Value.min.z, prefabBounds.Value.max.x, prefabBounds.Value.max.z);
			if (minRadius < 0f)
			{
				minRadius = 0f;
			}

			var min = prefabBounds.Value.min;
			var max = prefabBounds.Value.max;
			min.y += clearanceDistance;
			if (max.y < min.y)
			{
				max.y = min.y;
			}
		}

		bool foundValidSpawnPosition = false;
		Vector3 FloorSize = new Vector3(room.FloorAnchor.PlaneBoundary2D[0].x, 0f, room.FloorAnchor.PlaneBoundary2D[0].y);
		gameObject.transform.position = room.FloorAnchor.transform.position;
		//gameObject.transform.rotation = room.FloorAnchor.transform.rotation;
		GetComponent<BoxCollider>().size = FloorSize;

		return;
	}
}
