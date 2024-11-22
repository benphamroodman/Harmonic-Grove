#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Sets an instrument's Stereo Pan (-1 to 1).")]
	public class PMGInstrumentStereoPan : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;



		[RequiredField]
		[Tooltip("Normalized instrument stereo pan (-1 - 1).")]
		[HasFloatSlider(-1, 1)]
		public FsmFloat instrumentStereoPan;

		public bool everyFrame;

		
		public override void Reset()
		{
			instrumentIndex = null;
			instrumentStereoPan = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				  instrument.InstrumentData.StereoPan = instrumentStereoPan.Value;
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
				  instrument.InstrumentData.StereoPan = instrumentStereoPan.Value;
			}
		}

	}

}
#endif
