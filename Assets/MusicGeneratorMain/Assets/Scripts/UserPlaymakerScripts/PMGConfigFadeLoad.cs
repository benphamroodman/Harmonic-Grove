#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Fade out the current config, and load a new config by name.")]
	public class PMGConfigFadeLoad : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		//[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("A MusicGenerator config file to load.")]
		public FsmString configName;

		private Coroutine routine;

		
		public override void Reset()
		{
			configName = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				routine = StartCoroutine(LoadMe());
			}

			
		}


		private IEnumerator LoadMe()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			yield return mMusicGenerator.FadeLoadConfiguration( configName.Value );
			Finish();
		}
	}

}
#endif
