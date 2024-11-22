#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Get the current state of the Generator: Initializing, Ready, Stopped, Playing, Paused, etc")]
	public class PMGGeneratorState : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		//[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("Enum to store the Generator State.")]
		[ObjectType(typeof(ProcGenMusic.GeneratorState))]
		public FsmEnum storeGeneratorState;

	        [Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		
		public override void Reset()
		{
			storeGeneratorState = null;
			everyFrame = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeGeneratorState.Value = mMusicGenerator.GeneratorState;
			
			}
		}

		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeGeneratorState.Value = mMusicGenerator.GeneratorState;
			}

		}


	}

}
#endif
