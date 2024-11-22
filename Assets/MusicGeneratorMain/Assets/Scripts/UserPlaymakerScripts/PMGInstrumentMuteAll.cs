#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Toggle IsMuted on all Instruments.")]
	public class PMGInstrumentMuteAll : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("bool to toggle IsMuted on all Intruments.")]
		public FsmBool isMuted;

		
		public override void Reset()
		{
			instrumentIndex = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			if ( mMusicGenerator != null )
			{
		            int count = mMusicGenerator.InstrumentSet.Instruments.Count;

		            for (int i = 0; i < count; i++)
		                {
				var instrument = mMusicGenerator.InstrumentSet.Instruments[i];
				instrument.InstrumentData.IsMuted = isMuted.Value;
		                }

			}

			Finish();
		}




	}

}
#endif
