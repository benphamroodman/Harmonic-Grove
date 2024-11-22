#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Trigger an instrument by supplying the InstrumentIndex alone. PMG plays any note(s), chords, and succession notes according to the Instrument settings.")]
	public class PMGInstrumentPlaySuccession : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the instrument index.")]
		public FsmInt instrumentIndex;
		
		
		public override void Reset()
		{
			instrumentIndex = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];

				var step = Mathf.Max(mMusicGenerator.InstrumentSet.ProgressionStepsTaken, 0);
			
				// find our current chord progression step
				var progressionStep = mMusicGenerator.InstrumentSet.MusicGenerator.CurrentChordProgression[step];
			
				// Generate some notes
				var notes = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value].GetProgressionNotes(progressionStep, true);

				foreach (var note in notes)
					{
						// the GetProgressionNotes may return 'unplayed notes', we ignore those
						if (note == MusicConstants.UnplayedNote)
							{
								continue;
							}
						mMusicGenerator.PlayAudioClip(mMusicGenerator.InstrumentSet, instrument.InstrumentData.InstrumentType, note, instrument.InstrumentData.Volume, instrumentIndex.Value);

					}

				
					
			}
		Finish();
		}


	}

}
#endif
