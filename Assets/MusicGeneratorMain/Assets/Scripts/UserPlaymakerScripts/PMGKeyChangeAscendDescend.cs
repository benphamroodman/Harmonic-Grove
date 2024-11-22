#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Odds the key change will ascend/descend through the circle of fifths.")]
	public class PMGKeyChangeAscendDescend : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;



		[RequiredField]
		[Tooltip("Float to set the odds to (0 - 100).")]
		[HasFloatSlider(0, 100)]
		public FsmFloat keyChangeAscendDescend;

		
		public override void Reset()
		{
			keyChangeAscendDescend = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.KeyChangeAscendDescend = keyChangeAscendDescend.Value;
			}

			Finish();
		}

	}

}
#endif
