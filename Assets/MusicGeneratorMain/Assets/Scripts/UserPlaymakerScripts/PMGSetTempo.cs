#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	public class PMGSetTempo : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Float to set the tempo")]
		public FsmFloat tempoValue;

		
		public override void Reset()
		{
			tempoValue = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				mMusicGenerator.ConfigurationData.Tempo = tempoValue.Value;
			}

			Finish();
		}




	}

}
#endif
