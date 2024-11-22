#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Prevents the Groups from changing when the Progression re-rolls.")]
	public class PMGGroupChangeSuspend : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;


		[RequiredField]
		[Tooltip("the bool value to set GroupsAreTemporarilyOverridden.")]
		public FsmBool overrideGroups;

		public override void Reset()
		{
			overrideGroups = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.GroupsAreTemporarilyOverriden = overrideGroups.Value;

			}
			Finish();
		}


	}

}
#endif
