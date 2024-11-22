#if PMG_PLAYMAKER
UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("get the current Scale: Major, Nat Minor, Melodic Minor, etc.")]
	public class PMGGetScale : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Enum to store the scale.")]
		[ObjectType(typeof(ProcGenMusic.Scale))]
		public FsmEnum storeScale;

		
		public override void Reset()
		{
			storeScale = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeScale.Value = mMusicGenerator.ConfigurationData.Scale;
			}

			Finish();
		}




	}

}
#endif
