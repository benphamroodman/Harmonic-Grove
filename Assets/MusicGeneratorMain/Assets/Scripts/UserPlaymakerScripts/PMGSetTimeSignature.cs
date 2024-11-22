#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("SET the Time Signature: Four Four, Three Four, etc")]
	public class PMGSetTimeSignature : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		// [UIHint(UIHint.Variable)]
		[Tooltip("Enum to set the time signature.")]
		[ObjectType(typeof(ProcGenMusic.TimeSignatures))]
		public FsmEnum timeSignature;


		
		public override void Reset()
		{
			timeSignature = null;
		}


		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if ( mMusicGenerator != null )
			{


				if ( Equals(timeSignature.Value, ProcGenMusic.TimeSignatures.FourFour) )
				{
					
mMusicGenerator.InstrumentSet.SetTimeSignature( ProcGenMusic.TimeSignatures.FourFour);
				}

				if ( Equals(timeSignature.Value, ProcGenMusic.TimeSignatures.ThreeFour) )
				{
					
mMusicGenerator.InstrumentSet.SetTimeSignature( ProcGenMusic.TimeSignatures.ThreeFour);
				}

				if ( Equals(timeSignature.Value, ProcGenMusic.TimeSignatures.FiveFour) )
				{
					
mMusicGenerator.InstrumentSet.SetTimeSignature( ProcGenMusic.TimeSignatures.FiveFour);
				}
			}

			Finish();
		}




	}

}
#endif
