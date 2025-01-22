using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// detect index finger point to something with collider
/// </summary>
public class GetHandPointEvent : MonoBehaviour
{
	public UnityEvent OnEnterEvent;
	public UnityEvent StayEvent;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("wand") && OnEnterEvent != null)  // �ˬd�I�����O�_��wand
		{
			OnEnterEvent.Invoke();
			Debug.Log("finger OnTriggerEnter");
		} 
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("wand") && StayEvent != null)  // �ˬd�I�����O�_��wand
		{
			StayEvent.Invoke();
			Debug.Log("finger OnTriggerStay");
		}
	}
}
