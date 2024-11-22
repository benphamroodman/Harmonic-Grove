#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Suspends all Leitmotif Notes, Instruments play non-Leitmotif settings, but Harmonic Progression will use Leitmotif Odds.")]
	public class PMGLeitmotifNotesSuspend : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the bool value to set Lietmotif Is Temporarily Suspended.")]
		public FsmBool LeitmotifSuspended;

		public override void Reset()
		{
			LeitmotifSuspended = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.LeitmotifIsTemporarilySuspended = LeitmotifSuspended.Value;
				Finish();
			}
			Finish();
		}


	}

}
#endif
