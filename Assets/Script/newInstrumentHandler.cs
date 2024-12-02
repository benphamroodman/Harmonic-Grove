using UnityEngine;

namespace ProcGenMusic
{
	public class NewInstrumentHandler
	{
		public void Initialize(MusicGenerator musicGenerator, int instrumentIndex)
		{
			mMusicGenerator = musicGenerator;
			mInstrumentIndex = instrumentIndex;
		}

		public void SetInstrumentIndex(int instrumentIndex)
		{
			mInstrumentIndex = instrumentIndex;
		}

		/// <summary>
		/// Plays a randomly generated note that fits into the currently generated configuration
		/// </summary>
		public void PlayNote()
		{
			
			Debug.Log("PlayNote() was triggered!");
			
			// just index sanity check
			if (mInstrumentIndex >= mMusicGenerator.InstrumentSet.Instruments.Count)
			{
				Debug.Log("Something is wrong with the InstumentSet. mInstrumentIndex is " + mInstrumentIndex + 
					", while the InstrumentSet.count is " + mMusicGenerator.InstrumentSet.Instruments.Count + ".");
				return;
			}

			// here we grab an instrument
			var instrument = mMusicGenerator.InstrumentSet.Instruments[mInstrumentIndex];

			Debug.Log("passed step 1");
			
			// find our progression step (-1 is set on progression reset, hence the range checking)
			var step = Mathf.Max(mMusicGenerator.InstrumentSet.ProgressionStepsTaken, 0);

			Debug.Log("passed step 2");
			
			// find our current chord progression step
			var progressionStep = mMusicGenerator.InstrumentSet.MusicGenerator.CurrentChordProgression[step];

			Debug.Log("passed step 3");
			
			// Generate some notes
			var notes = mMusicGenerator.InstrumentSet.Instruments[mInstrumentIndex].GetProgressionNotes(progressionStep, true);

			Debug.Log("passed step 4");

			foreach (var note in notes)
			{
				// the GetProgressionNotes may return 'unplayed notes', we ignore those
				if (note == MusicConstants.UnplayedNote)
				{
					continue;
					Debug.Log("Unplayed not");
				}

				Debug.Log("passed step 5");

				// directly play a note through the music generator
				mMusicGenerator.PlayAudioClip(mMusicGenerator.InstrumentSet, instrument.InstrumentData.InstrumentType, note,
					instrument.InstrumentData.Volume,
					mInstrumentIndex);
				Debug.Log("passed step 6");
			}
		}

		/// <summary>
		/// Plays a specific note through the music generator
		/// </summary>
		/// <param name="index"></param>
		public void PlaySpecificNote(int index)
		{
			// instrument range check
			if (mInstrumentIndex >= mMusicGenerator.InstrumentSet.Instruments.Count)
			{
				return;
			}

			// grab our instrument
			var instrument = mMusicGenerator.InstrumentSet.Instruments[mInstrumentIndex];
			
			// grab the specific note by index
			var note = mMusicGenerator.InstrumentSet.Instruments[mInstrumentIndex].NoteGenerators[2].GetSpecificNote(index, 1);

			if (note == MusicConstants.UnplayedNote)
			{
				return;
			}
			
			// play the note directly through the music generator
			mMusicGenerator.PlayNote(mMusicGenerator.InstrumentSet, instrument.InstrumentData.Volume, instrument.InstrumentData.InstrumentType, note,
				mInstrumentIndex);
		}

		private int mInstrumentIndex;
		private MusicGenerator mMusicGenerator;
	}
}