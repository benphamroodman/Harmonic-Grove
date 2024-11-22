#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Toggle the Manual Beats for an instrument.")]
	public class PMGInstrumentManualBeats : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("bool to toggle Manual Beats on the Instrument.")]
		public FsmBool manualBeats;

		
		public override void Reset()
		{
			instrumentIndex = null;
			manualBeats = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				 instrument.InstrumentData.ForceBeat = manualBeats.Value;
			}

			Finish();
		}




	}

}
#endif
