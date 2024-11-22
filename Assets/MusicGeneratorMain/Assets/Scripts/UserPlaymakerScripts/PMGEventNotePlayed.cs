#if PMG_PLAYMAKER
using UnityEngine;
using UnityEngine.Events;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Procedural Music Generator")]
	[Tooltip("Fires an Event when the Generator plays a note, stores args (InstrumentIndex, Note, Volume, InstrumentName). Option to suppress playing the note if handled by 3rd-party, midi, or another synth.")]
	public class PMGEventNotePlayed : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(MusicGenerator))]
		[Tooltip("the target. A MusicGenerator component is required.")]
		public FsmOwnerDefault gameObject;

		//[RequiredField]
		[Tooltip("the instrument index that played.")]
		[UIHint(UIHint.Variable)]
		public FsmInt instrumentIndex;

		//[RequiredField]
		[Tooltip("the note that played (0 - 35).")]
		[UIHint(UIHint.Variable)]
		public FsmInt noteIndex;

		//[RequiredField]
		[Tooltip("the note volume")]
		[UIHint(UIHint.Variable)]
		public FsmFloat noteVolume;

		//[RequiredField]
		[Tooltip("the name of the instrument")]
		[UIHint(UIHint.Variable)]
		public FsmString instrumentName;

		[RequiredField]
		[Tooltip("the event on Note Played")]
		public FsmEvent notePlayedEvent;

		//[RequiredField]
		[Tooltip("Suppress playing the notes")]
		public FsmBool suppressNote;
		
		
		public override void Reset()
		{
			instrumentIndex = null;
			noteIndex = null;
			noteVolume = null;
			instrumentName = null;
			notePlayedEvent = null;
			suppressNote = null;
		}



		// Code that runs every frame.
		public override void OnEnter()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();

			mMusicGenerator.NotePlayed += OnNotePlayed;

		}

		public override void OnExit()
		{
			MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
			mMusicGenerator.NotePlayed -= OnNotePlayed;
		}


		private bool OnNotePlayed( object source, NotePlayedArgs args )
		{
			instrumentIndex.Value = args.InstrumentIndex;
			noteIndex.Value = args.Note;
			noteVolume.Value = args.Volume;
			instrumentName.Value = args.InstrumentName;
			Fsm.Event(notePlayedEvent);
			if ( suppressNote.Value == true )
				{
					return false;
				}
				else
					return true;

		}

	}

}
#endif
