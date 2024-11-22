using System;
using UnityEngine;

#pragma warning disable 0649

namespace ProcGenMusic
{
	[Serializable]
	public struct LeitmotifNote
	{
		public LeitmotifNote( int scaledNote = -1, int sharpFlat = 0 )
		{
			ScaledNote = scaledNote;
			Accidental = sharpFlat;
		}

		public int GetScaledNote()
		{
			return ScaledNote;
		}

		public int GetAccidental()
		{
			return Accidental;
		}

		[SerializeField] private int ScaledNote;
		[SerializeField] private int Accidental;
	}
}
