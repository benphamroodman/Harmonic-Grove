#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Set the current Scale: Major, Nat Minor, Melodic Minor, etc.")]
	public class PMGSetScale : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Enum to set the scale.")]
		[ObjectType(typeof(ProcGenMusic.Scale))]
		[UIHint(UIHint.Variable)]
		public FsmEnum scaleValue;

		
		public override void Reset()
		{
			scaleValue = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.Scale = (ProcGenMusic.Scale)scaleValue.Value;
			}

			Finish();
		}




	}

}
#endif
