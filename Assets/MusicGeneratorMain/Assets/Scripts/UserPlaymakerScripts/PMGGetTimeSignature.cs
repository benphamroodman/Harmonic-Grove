#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Get the current Time Signature: Four Four, Three Four, etc")]
	public class PMGGetTimeSignature : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Enum to store the time signature.")]
		[ObjectType(typeof(ProcGenMusic.TimeSignatures))]
		public FsmEnum storeTimeSignature;

		
		public override void Reset()
		{
			storeTimeSignature = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{
				storeTimeSignature.Value = mMusicGenerator.InstrumentSet.Data.TimeSignature;
			}

			Finish();
		}




	}

}
#endif
