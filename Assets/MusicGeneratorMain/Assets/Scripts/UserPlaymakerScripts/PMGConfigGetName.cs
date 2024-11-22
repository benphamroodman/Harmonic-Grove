#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Store the name of the current config to a string variable.")]
	public class PMGConfigGetName : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		//[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("string to store the config name.")]
		public FsmString storeConfig;

		
		public override void Reset()
		{
			storeConfig = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeConfig.Value = mMusicGenerator.ConfigurationData.ConfigurationName;
			}

			Finish();
		}




	}

}
#endif
