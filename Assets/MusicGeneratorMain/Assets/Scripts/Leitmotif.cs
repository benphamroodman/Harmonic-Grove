using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
#nullable enable

namespace ProcGenMusic
{
	/// <summary>
	/// Our Leitmotif. Handles preset notes to integrate into normal music generation.
	/// </summary>
	[Serializable]
	public class Leitmotif
	{
		[Serializable]
		public struct LeitmotifKey
		{
			public LeitmotifKey( int setMeasure, int setTimestep, int setNoteIndex )
			{
				measure = setMeasure;
				timestep = setTimestep;
				noteIndex = setNoteIndex;
			}

			public int Measure => measure;
			public int Timestep => timestep;
			public int NoteIndex => noteIndex;

			[SerializeField] private int measure;
			[SerializeField] private int timestep;
			[SerializeField] private int noteIndex;
		}

		[Serializable]
		public class LeitmotifNotes
		{
			public LeitmotifNotes( List<LeitmotifNote> setNotes )
			{
				notes = setNotes;
			}

			[SerializeField] private List<LeitmotifNote> notes;

			public List<LeitmotifNote> Notes => notes;
			public IReadOnlyList<LeitmotifNote> NotesReadonly => notes;
		}

		public Leitmotif Clone()
		{
			var clone = (Leitmotif) MemberwiseClone();
			clone.notesDictionary = new();
			foreach ( var note in notesDictionary )
			{
				clone.notesDictionary.Add( note.Key, note.Value );
			}

			clone.IsEnabled = IsEnabled;
			return clone;
		}

		/// <summary>
		/// Whether this leitmotif is enabled
		/// </summary>
		public bool IsEnabled;

		[Serializable]
		public class LeitmotifMeasure
		{
			public List<LeitmotifTimeStep> Beat = new();
		}

		[Serializable]
		public class LeitmotifNoteIndex
		{
			public List<LeitmotifNote> notes = new();
		}

		[Serializable]
		public class LeitmotifTimeStep
		{
			public List<LeitmotifNoteIndex> SubBeat = new();
		}

		[Obsolete( "Please use NotesDictionary. Notes will be removed in a future version." )]
		public List<LeitmotifMeasure>? Notes;

		[SerializeField]
		private SerializableDictionary<LeitmotifKey, LeitmotifNotes> notesDictionary = new();

		public IEnumerable<(LeitmotifKey Key, IReadOnlyList<LeitmotifNote> leitmotifNotes)> NotesDictionaryReadonly()
		{
			foreach ( var notesEntry in notesDictionary )
			{
				yield return ( notesEntry.Key, notesEntry.Value.NotesReadonly );
			}
		}

		public bool TryGetLeitmotifNotes( int measureIndex, int timestep, int noteIndex, out IReadOnlyList<LeitmotifNote> leitmotifNotes )
		{
			leitmotifNotes = null!;
			if ( notesDictionary.TryGetValue( new LeitmotifKey( measureIndex, timestep, noteIndex ), out var entry ) )
			{
				leitmotifNotes = entry.NotesReadonly;
				return true;
			}

			return false;
		}

		public void AddLeitmotifNotes( int measureIndex, int timestep, int noteIndex, LeitmotifNotes notes )
		{
			notesDictionary.Add( new LeitmotifKey( measureIndex, timestep, noteIndex ), notes );
		}

		public void AddLeitmotifNote( int measureIndex, int timestep, int noteIndex, LeitmotifNote note )
		{
			if ( notesDictionary.TryGetValue( new LeitmotifKey( measureIndex, timestep, noteIndex ), out var notes ) )
			{
				notes.Notes.Add( note );
			}
			else
			{
				notesDictionary[ new LeitmotifKey( measureIndex, timestep, noteIndex )] = new LeitmotifNotes( new List<LeitmotifNote> { note } );
			}
		}

		public void RemoveLeitmotifNote( int measureIndex, int timestep, int noteIndex, LeitmotifNote note )
		{
			var key = new LeitmotifKey( measureIndex, timestep, noteIndex );
			var removeEntry = false;
			if ( notesDictionary.TryGetValue( new LeitmotifKey( measureIndex, timestep, noteIndex ), out var notes ) )
			{
				notes.Notes.Remove( note );
				if ( notes.Notes.Count == 0 )
				{
					removeEntry = true;
				}
			}

			if ( removeEntry )
			{
				notesDictionary.Remove( key );
			}
		}

		public static int[] GetUnscaledNoteArray( IReadOnlyList<LeitmotifNote> leitmotifNotes, MusicGenerator musicGenerator )
		{
			var notes = new int[leitmotifNotes.Count];
			for ( var index = 0; index < leitmotifNotes.Count; index++ )
			{
				notes[index] = GetUnscaledNoteIndex( leitmotifNotes[index], musicGenerator );
			}

			return notes;
		}

		public static int GetUnscaledNoteIndex( LeitmotifNote note, MusicGenerator musicGenerator )
		{
			var scaledNote = note.GetScaledNote();
			if ( scaledNote == MusicConstants.UnplayedNote )
			{
				return scaledNote;
			}

			var scale = MusicConstants.GetScale( musicGenerator.ConfigurationData.Scale );
			var mode = (int) musicGenerator.ConfigurationData.Mode;
			var unscaledIndex = 0;

			for ( var index = 0; index < scaledNote; index++ )
			{
				unscaledIndex += scale[MusicConstants.SafeLoop( mode + index, 0, MusicConstants.ScaleLength )];
			}

			unscaledIndex += (int) musicGenerator.ConfigurationData.Key;
			unscaledIndex += note.GetAccidental();

			return MusicConstants.SafeLoop( unscaledIndex, 0, MusicConstants.MaxInstrumentNotes );
		}
	}
}
