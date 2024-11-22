#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("mutes all Instruments but the solo Instrument, and temporarily forces the solo Instrument's Group IsPlaying for the current Progression.")]
	public class PMGInstrumentMuteSolo : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the solo Instrument.")]
		public FsmInt instrumentIndex;


		
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
					instrument.InstrumentData.IsMuted = true;
		                }
				var solomio = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				solomio.InstrumentData.IsMuted = false;
				mMusicGenerator.InstrumentSet.OverrideGroupIsPlaying(solomio.InstrumentData.Group, true);
				
			}

			Finish();
		}




	}

}
#endif
