#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Event when the Music Generator has reached a new progression. Useful for synchronizing effects with the config's compositional structure.")]
	public class PMGEventProgressionGenerated : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("event when progression has generated.")]
		public FsmEvent progressionEvent;

		public override void Reset()
		{
			progressionEvent = null;
		}

		// Code that runs every frame.
		public override void OnUpdate()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			mMusicGenerator.ProgressionGenerated.AddListener(onProgression);

		}

		private void onProgression()
		{
			Fsm.Event(progressionEvent);
		}

	}

}
#endif
