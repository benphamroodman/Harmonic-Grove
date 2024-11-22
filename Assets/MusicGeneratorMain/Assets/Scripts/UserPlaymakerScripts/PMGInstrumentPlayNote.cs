#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Play a specific note by supplying an InstrumentIndex and Note value. Useful for memory games, expressive solos, etc.")]
	public class PMGInstrumentPlayNote : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the instrument index.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("the note to play (0 - 35).")]
		public FsmInt noteToPlay;

		
		public override void Reset()
		{
			instrumentIndex = null;
			noteToPlay = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				var note = noteToPlay.Value;
				if (instrument.InstrumentData.IsPercussion)
				{
					note = 0;
				}


				mMusicGenerator.PlayAudioClip(mMusicGenerator.InstrumentSet, instrument.InstrumentData.InstrumentType, note,
					instrument.InstrumentData.Volume,
					instrumentIndex.Value);
			}
			Finish();
		}


	}

}
#endif
