#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Leitmotif notes will play regardless of the Instruments' Groups.")]
	public class PMGLeitmotifIgnoresGroups : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the bool value to set Lietmotif Is Temporarily Suspended.")]
		public FsmBool leitmotifIgnoresGroups;

		public override void Reset()
		{
			leitmotifIgnoresGroups = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.ConfigurationData.LeitmotifIgnoresGroups = leitmotifIgnoresGroups.Value;
				Finish();
			}
			Finish();
		}


	}

}
#endif
