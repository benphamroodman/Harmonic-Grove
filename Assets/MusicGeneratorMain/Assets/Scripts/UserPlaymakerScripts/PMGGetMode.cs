#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Get the current Mode: Ionian, Dorian, Phrygian, Lydian, etc.")]
	public class PMGGetMode : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Enum to store the mode.")]
		[ObjectType(typeof(ProcGenMusic.Mode))]
		public FsmEnum storeMode;

		
		public override void Reset()
		{
			storeMode = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeMode.Value = mMusicGenerator.ConfigurationData.Mode;
			}

			Finish();
		}




	}

}
#endif
