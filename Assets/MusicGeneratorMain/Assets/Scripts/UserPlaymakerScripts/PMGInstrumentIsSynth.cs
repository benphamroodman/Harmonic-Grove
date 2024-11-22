#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Bool check returns true if an instrument is a Synth.")]
	public class PMGInstrumentIsSynth : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("bool is true when the instrument IsSynth.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isSynth;

		
		public override void Reset()
		{
			instrumentIndex = null;
			isSynth = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				isSynth.Value = instrument.InstrumentData.IsSynth;
			}

			Finish();
		}




	}

}
#endif
