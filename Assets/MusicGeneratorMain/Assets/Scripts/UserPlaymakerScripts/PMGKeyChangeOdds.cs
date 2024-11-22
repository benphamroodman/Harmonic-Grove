#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Set the KeyChangeOdds.")]
	public class PMGKeyChangeOdds : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Float to set the odds to (0 - 100).")]
		[HasFloatSlider(0, 100)]
		public FsmFloat keyChangeOdds;

		
		public override void Reset()
		{
			keyChangeOdds = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.KeyChangeOdds = keyChangeOdds.Value;
			}

			Finish();
		}

	}

}
#endif
