#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("SET Leitmotif and Theme Repeat Odds.")]
	public class PMGLeitmotifThemeOdds : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Float to set the Leitmotif PlayThemeOdds (0 - 100).")]
		[HasFloatSlider(0, 100)]
		public FsmFloat themeOdds;

		
		public override void Reset()
		{
			themeOdds = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.PlayThemeOdds = themeOdds.Value;
			}

			Finish();
		}




	}

}
#endif
