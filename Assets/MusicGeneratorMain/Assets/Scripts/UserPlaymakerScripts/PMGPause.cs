#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Pause the Generator.")]
	public class PMGPause : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			UnityEngine.GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.Pause();
			}
			Finish();
		}


	}


}
#endif
