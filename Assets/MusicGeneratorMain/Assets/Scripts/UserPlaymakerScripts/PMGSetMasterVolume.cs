#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Set the master volume.")]
	public class PMGSetMasterVolume : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the float value to set the volume to.")]
		public FsmFloat volumeValue;

		public override void Reset()
		{
			volumeValue = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.SetVolume(volumeValue.Value);
				Finish();
			}
			Finish();
		}


	}

}
#endif
