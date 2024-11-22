#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Bool check returns true if Leitmotif is playing.")]
	public class PMGLeitmotifIsPlaying : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;


		[RequiredField]
		[Tooltip("bool is true when the Leitmotif is playing.")]
		[UIHint(UIHint.Variable)]
		public FsmBool leitmotifIsPlaying;

		public bool everyFrame;

		
		public override void Reset()
		{
			leitmotifIsPlaying = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null)
			{
				if (mMusicGenerator.CurrentChordProgression.Count != 4)
				{
				
					leitmotifIsPlaying.Value = true;
				}
				else
				{
					leitmotifIsPlaying.Value = false;
				}
			}

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null)
			{
				if (mMusicGenerator.CurrentChordProgression.Count != 4)
				{
				
					leitmotifIsPlaying.Value = true;
				}
				else
				{
					leitmotifIsPlaying.Value = false;
				}
			}
		}



	}

}
#endif
