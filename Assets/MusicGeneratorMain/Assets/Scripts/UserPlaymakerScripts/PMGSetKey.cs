#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Set the current Key: C, C Sharp, D, D Sharp, E, F, etc")]
	public class PMGSetKey : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Enum to set the key.")]
		[ObjectType(typeof(ProcGenMusic.Key))]
		[UIHint(UIHint.Variable)]
		public FsmEnum keyValue;

		
		public override void Reset()
		{
			keyValue = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.Key = (ProcGenMusic.Key)keyValue.Value;
			}

			Finish();
		}




	}

}
#endif
