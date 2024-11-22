#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Get the current Tempo.")]
	public class PMGGetTempo : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Float to store the tempo.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeTempo;

		
		public override void Reset()
		{
			storeTempo = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeTempo.Value = mMusicGenerator.ConfigurationData.Tempo;
			}

			Finish();
		}




	}

}
#endif
