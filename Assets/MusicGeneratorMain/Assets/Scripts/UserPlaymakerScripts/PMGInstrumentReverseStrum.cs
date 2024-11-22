#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("SET ReverseStrum on an instrument by InstrumentIndex.")]
	public class PMGInstrumentReverseStrum : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("bool to set Reverse Strum on the Instrument.")]
		public FsmBool reverseStrum;

		
		public override void Reset()
		{
			instrumentIndex = null;
			reverseStrum = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				 instrument.InstrumentData.ReverseStrum = reverseStrum.Value;
			}

			Finish();
		}




	}

}
#endif
