#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Free-play an instrument by supplying the InstrumentIndex, Note, and Volume. Useful for creating interactive musical instruments.")]
	public class PMGInstrumentPlay : FsmStateAction
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

		[RequiredField]
		[Tooltip("the note volume")]
		[HasFloatSlider(0, 1)] 
		public FsmFloat noteVolume;

		public override void Reset()
		{
			instrumentIndex = null;
			noteToPlay = null;
			noteVolume = null;
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


				mMusicGenerator.PlayNote(mMusicGenerator.InstrumentSet, noteVolume.Value, instrument.InstrumentData.InstrumentType, note, instrumentIndex.Value);
			}
			Finish();
		}


	}

}
#endif
