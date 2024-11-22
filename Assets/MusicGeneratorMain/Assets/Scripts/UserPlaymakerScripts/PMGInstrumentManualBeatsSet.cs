#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("SET the Manual Beats of an instrument with an Array of bools.")]
	public class PMGInstrumentManualBeatsSet : FsmStateAction
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
				var instrument = mMusicGenerator.InstrumentSet.Instruments[instrumentIndex.Value];
				
				for (int i = 0; i < 20; i++)
				{
					bool mybeat = (bool)manualBeats.Get(i);
					instrument.InstrumentData.ForcedBeats[i] = mybeat;
					
				}
			}

				Finish();

		}



	}

}
#endif
