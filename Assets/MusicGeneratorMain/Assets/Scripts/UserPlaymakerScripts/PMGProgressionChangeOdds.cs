#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Odds the Harmonic Progression will roll a new random pattern of 4 chords. 100 will always roll a new Progression, 0 will repeat the same Progression chords.")]
	public class PMGProgressionChangeOdds : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("the Float value to set the progressionChangeOdds to (0 - 100).")]
		[HasFloatSlider(0, 100)]
		public FsmFloat progressionChangeOdds;

		public override void Reset()
		{
			progressionChangeOdds = null;
		}

		// Code that runs on entering the state.

		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			if (mMusicGenerator != null)
			{
				mMusicGenerator.ConfigurationData.ProgressionChangeOdds = progressionChangeOdds.Value;
				Finish();
			}
			Finish();
		}


	}

}
#endif
