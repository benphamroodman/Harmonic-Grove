#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Reads the currently playing Groups by Index and sets a bool for each.")]
	public class PMGGroupIsPlaying : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("isPlaying bool.")]
		public FsmBool groupOne;

		[RequiredField]
		[Tooltip("isPlaying bool.")]
		public FsmBool groupTwo;

		[RequiredField]
		[Tooltip("isPlaying bool.")]
		public FsmBool groupThree;

		[RequiredField]
		[Tooltip("isPlaying bool.")]
		public FsmBool groupFour;

		[RequiredField]
		[Tooltip("every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			groupOne = null;
			groupTwo = null;
			groupThree = null;
			groupFour = null;
			everyFrame = false;
		}


		// Code that runs on entering the state.

        	public override void OnEnter()
        	{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				groupOne.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[0];
				groupTwo.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[1];
				groupThree.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[2];
				groupFour.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[3];

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
				groupOne.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[0];
				groupTwo.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[1];
				groupThree.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[2];
				groupFour.Value = mMusicGenerator.InstrumentSet.GroupIsPlaying[3];

			}
		}



	}

}
#endif
