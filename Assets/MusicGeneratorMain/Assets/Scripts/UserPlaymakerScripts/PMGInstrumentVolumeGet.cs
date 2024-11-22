#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("GET a normalized (0 - 1) instrument volume.")]
	public class PMGInstrumentVolumeGet : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("Normalized instrument volume (0 - 1).")]
		[UIHint(UIHint.Variable)]
		public FsmFloat instrumentVolume;

		public bool everyFrame;

		
		public override void Reset()
		{
			instrumentIndex = null;
			instrumentVolume = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				 instrumentVolume.Value = instrument.InstrumentData.Volume;
			}

			if (!everyFrame)
			{
			Finish();
			}
		}


		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				 instrumentVolume.Value = instrument.InstrumentData.Volume;
			}


		}

	}

}
#endif
