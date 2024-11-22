using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

namespace ProcGenMusic
{
	/// <summary>
	/// Percussion Editor Instrument for the Leitmotif Editor
	/// </summary>
	public class LeitmotifPercussionEditorInstrument : PercussionEditorInstrument
	{
		protected override int RepeatCount => mUIManager == null ? 0 : mUIManager.CurrentInstrumentSet.RepeatCount;
		protected override int NumMeasures => mUIManager == null ? 0 : mUIManager.MusicGenerator.ConfigurationData.NumLeitmotifMeasures;

		///<inheritdoc/>
		protected override void InitializeInstrument( Instrument instrument, Action<bool> callback = null )
		{
			mInstrument = instrument;
			foreach (var noteEntry in instrument.InstrumentData.Leitmotif.NotesDictionaryReadonly())
			{
				AddNote( noteEntry.Key.Timestep, noteEntry.Key.NoteIndex, noteEntry.Key.Measure);
			}

			mDragElement.Initialize( () => { mOnInstrumentMoved.Invoke(); },
				mUIManager );
			mInstrument = instrument;
			RefreshInstrument();
			callback?.Invoke( true );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="timestepIdx"></param>
		/// <param name="noteIdx"></param>
		/// <param name="measureIdx"></param>
		private void AddNote( int timestepIdx, int noteIdx, int measureIdx )
		{
			var timestep = new Vector2Int( timestepIdx, noteIdx );
			mInstrumentDisplay.GetOffsetAndNoteIndex(
				measureIdx,
				timestep,
				note: 0,
				out var offsetPosition );
			if ( offsetPosition.y > 0 )
			{
				mInstrumentDisplay.AddOrRemoveNote(
					measureIdx,
					CurrentMeasure,
					mInstrument,
					new MeasureEditorNoteData( timestep, noteIndex: 0, CurrentMeasure, offsetPosition )
				);
			}
		}
	}
}