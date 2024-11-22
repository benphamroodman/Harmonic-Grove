#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("SET an instrument's normalized volume (0 - 1) relative to the master volume.")]
	public class PMGInstrumentVolumeSet : FsmStateAction
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
		[HasFloatSlider(0, 1)]
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
				  instrument.InstrumentData.Volume = instrumentVolume.Value;
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
				  instrument.InstrumentData.Volume = instrumentVolume.Value;
			}

		}
	}

}
#endif
