#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Event when the Music Generator has begun a new Measure. Useful for synchronizing effects with the config's compositional structure.")]
	public class PMGEventNewMeasure : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("event when a Measure has generated.")]
		public FsmEvent measureEvent;

		float previousValue;

		public override void Reset()
		{
			measureEvent = null;
		}

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			previousValue = mMusicGenerator.InstrumentSet.MeasureStartTimer;
		}

		// Code that runs every frame.
		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			if ( mMusicGenerator.InstrumentSet.MeasureStartTimer != previousValue )
			{
				Fsm.Event(measureEvent);
			}


		}

	}

}
#endif
