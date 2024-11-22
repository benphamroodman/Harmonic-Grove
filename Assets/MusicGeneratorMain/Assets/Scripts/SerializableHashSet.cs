using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGenMusic
{
	[Serializable]
	public class SerializableHashSet<TKey> : HashSet<TKey>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TKey> keys = new();

		// save the dictionary to lists
		public void OnBeforeSerialize()
		{
			keys.Clear();
			foreach ( var key in this )
			{
				keys.Add( key );
			}
		}

		// load dictionary from lists
		public void OnAfterDeserialize()
		{
			Clear();

			foreach ( var key in keys )
			{
				Add( key);
			}
		}
	}
}
