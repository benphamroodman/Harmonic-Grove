#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Override a Group's playing status for the current Progression. Resets on the next Progression, according to Config rules.")]
	public class PMGGroupTemporaryOveride : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Index of the Group (0 - 3).")]
		public FsmInt groupIndex;

		[RequiredField]
		[Tooltip("isPlaying bool.")]
		public FsmBool isPlaying;
		
		public override void Reset()
		{
			isPlaying = null;
			groupIndex = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			if ( mMusicGenerator != null )
			{

				mMusicGenerator.InstrumentSet.OverrideGroupIsPlaying(groupIndex.Value,isPlaying.Value);

			}

			Finish();
		}

	}

}
#endif
