#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Set the current Mode: Ionian, Dorian, Phrygian, Lydian, etc.")]
	public class PMGSetMode : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Enum to set the mode.")]
		[ObjectType(typeof(ProcGenMusic.Mode))]
		[UIHint(UIHint.Variable)]
		public FsmEnum modeValue;

		
		public override void Reset()
		{
			modeValue = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.Mode = (ProcGenMusic.Mode)modeValue.Value;
			}

			Finish();
		}




	}

}
#endif
