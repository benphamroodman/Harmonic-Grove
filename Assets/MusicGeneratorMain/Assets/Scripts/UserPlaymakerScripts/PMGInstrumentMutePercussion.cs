#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Mutes all Instruments based on isPercussion.")]
	public class PMGInstrumentMutePercussion : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the solo Instrument.")]
		public FsmBool mutePercussion;

		[RequiredField]
		[Tooltip("Index of the solo Instrument.")]
		public FsmBool muteNonpercussion;
		
		public override void Reset()
		{
			mutePercussion = null;
			muteNonpercussion = null;
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
					if (instrument.InstrumentData.IsPercussion)
					{
						instrument.InstrumentData.IsMuted = mutePercussion.Value;
					}

					if (!instrument.InstrumentData.IsPercussion)
					{
						instrument.InstrumentData.IsMuted = muteNonpercussion.Value;
					}
		                }

				
			}

			Finish();
		}


	}

}
#endif
