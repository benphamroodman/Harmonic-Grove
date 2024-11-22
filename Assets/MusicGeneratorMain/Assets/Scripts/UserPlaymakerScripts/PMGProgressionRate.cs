#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory( "Procedural Music Generator" )]
	[Tooltip( "Set the current Progression Rate (the number of beats per progression: 4, 8, 16, etc). Use with Caution: can break melody patterns." )]
	public class PMGProgressionRate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent( typeof( MusicGenerator ) )]
		[Tooltip( "the target. A MusicGenerator component is required." )]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip( "the int value to set the progression rate to." )]
		public FsmInt progressionRate;

		public override void Reset()
		{
			progressionRate = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget( gameObject ).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.ProgressionRate = progressionRate.Value;
				Finish();
			}

			Finish();
		}
	}
}

#endif
