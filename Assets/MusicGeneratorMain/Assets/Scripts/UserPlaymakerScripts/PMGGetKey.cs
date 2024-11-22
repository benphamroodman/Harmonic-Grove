#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Get the current Key: C, C Sharp, D, D Sharp, E, F, etc")]
	public class PMGGetKey : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Enum to store the key.")]
		[ObjectType(typeof(ProcGenMusic.Key))]
		public FsmEnum storeKey;

		public bool everyFrame;
		
		public override void Reset()
		{
			storeKey = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeKey.Value = mMusicGenerator.ConfigurationData.Key;
			}

			if (!everyFrame)
			{
			Finish();
			}
		}

		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeKey.Value = mMusicGenerator.ConfigurationData.Key;
			}
		}
	}

}
#endif
