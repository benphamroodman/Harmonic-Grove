#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("GET an Array of the Manual Beats of an Instrument.")]
	public class PMGInstrumentManualBeatsGet : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the instrument.")]
		public FsmInt instrumentIndex;

		[RequiredField]
		[Tooltip("Array to store the Manual Beats.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Bool)] 
		public FsmArray manualBeats;



		
		public override void Reset()
		{
			manualBeats = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				manualBeats.Reset();
				manualBeats.Resize(20);
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				
				for (int i = 0; i < 20; i++)
				{
					bool mybeat = instrument.InstrumentData.ForcedBeats[i];
					manualBeats.Set(i, mybeat);
				}
			}

				Finish();

		}


	}

}
#endif
