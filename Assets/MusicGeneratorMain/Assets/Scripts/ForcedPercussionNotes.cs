using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#pragma warning disable 0649
#nullable enable

namespace ProcGenMusic
{
	/// <summary>
	/// Data class representing all percussion notes for forced percussion for an instrument.
	/// TODO: this is a terrible setup requiring large containers of mostly unused integers.
	/// </summary>
	[Serializable]
	public class ForcedPercussionNotes
	{
		[Serializable, Obsolete]
		public class PercussionTimestep
		{
			public bool[] Notes => mNotes;

			[SerializeField] private bool[] mNotes = new bool[MusicConstants.MaxStepsPerTimestep];
		}

		[Serializable, Obsolete]
		public class PercussionMeasure
		{
			public PercussionTimestep[] Timesteps => mTimesteps;

			[SerializeField] private PercussionTimestep[] mTimesteps = { new PercussionTimestep(), new PercussionTimestep(), new PercussionTimestep(), new PercussionTimestep() };
		}

		[SerializeField, Obsolete] private PercussionMeasure[]? mMeasures = { new PercussionMeasure(), new PercussionMeasure(), new PercussionMeasure(), new PercussionMeasure() };

		[Obsolete] public PercussionMeasure[]? Measures => mMeasures;

		public void ClearObsoleteMeasures()
		{
#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS0612 // Type or member is obsolete
			mMeasures = null;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
		}

		public IReadOnlyCollection<PercussionKey> ForcedNotes => forcedNotes;
		[SerializeField] private SerializableHashSet<PercussionKey> forcedNotes = new();

		public bool IsBeatForced(int measure, int timestep, int subBeat)
		{
			return forcedNotes.Contains(new PercussionKey(measure, timestep, subBeat));
		}

		public void SetForcedBeat( int measure, int timestep, int subBeat, bool isEnabled )
		{
			if ( !isEnabled )
			{
				forcedNotes.Remove( new PercussionKey( measure, timestep, subBeat ) );
			}
			else
			{
				forcedNotes.Add( new PercussionKey( measure, timestep, subBeat ) );
			}
		}

		[Serializable]
		public struct PercussionKey
		{
			public PercussionKey( int setMeasure, int setBeat, int setBeatStep )
			{
				measure = setMeasure;
				beat = setBeat;
				beatStep = setBeatStep;
			}

			public int Measure => measure;
			public int Beat => beat;
			public int BeatStep => beatStep;

			[SerializeField] private int measure;
			[FormerlySerializedAs("timestep")]
			[SerializeField] private int beat;
			[FormerlySerializedAs("subBeat")]
			[SerializeField] private int beatStep;
		}
	}
}
